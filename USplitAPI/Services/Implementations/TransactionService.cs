using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;
using USplitAPI.Services.Strategies.Transaction;

namespace USplitAPI.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly DebtStrategyFactory _debtStrategyFactory;
    private readonly USplitDBContext _context;
    private readonly IFamilyService _familyService;

    public TransactionService(IMapper mapper, USplitDBContext context, DebtStrategyFactory debtStrategyFactory, IFamilyService familyService)
    {
        _familyService = familyService;
        _mapper = mapper;
        _context = context;
        _debtStrategyFactory = debtStrategyFactory;
    }
    
    public async Task<ResultTuple> GetUserDebtsAsync(int userId, int familyId)
    {
        var foundFamily = await _context.UserFamilies.SingleOrDefaultAsync(e => e.FamilyId == familyId && e.UserId == userId);
        if (foundFamily == null) return ResultTuple.Exception(StatusCodes.Status404NotFound, "Family does not exist.");

        if (foundFamily.UserId != userId) return ResultTuple.Exception(StatusCodes.Status401Unauthorized);

        var foundDebts = await _context.Debts
            .Where(d => d.OwnerUserId == userId && d.OwnerFamilyId == familyId)
            .ProjectTo<DebtDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return ResultTuple.Success(foundDebts);
    }

    public async Task<ResultTuple> AddTransaction(int transactionUserId, TransactionOptionsDto options)
    {
        if (transactionUserId != options.UserId) return ResultTuple.Exception(StatusCodes.Status404NotFound, "User cannot post transactions in name of other users");

        var isMember = (await _familyService.IsMemberAsync(options.UserId, options.FamilyId)).Result<bool>();
        if (!isMember) return ResultTuple.Exception(StatusCodes.Status401Unauthorized, "Cannot add transaction to family which user is not a part of.");
        
        var debtCreationStrategy = _debtStrategyFactory.GetDebtCreationStrategy(options.SplitType);
        if (debtCreationStrategy == null) return ResultTuple.Exception(StatusCodes.Status400BadRequest, "Split type not valid.");
        
        var debtsResultTuple = debtCreationStrategy.CreateDebts(options);
        if (debtsResultTuple.result == null) return debtsResultTuple;

        var createdDebts = debtsResultTuple.Result<List<DebtEntity>>();

        var transactionUserDebts = await _context.Debts
            .Where(e => !e.IsPaid && e.OwnerUserId == options.UserId)
            .ToListAsync();

        foreach (var transactionUserDebt in transactionUserDebts)
        {
            var endUserDebt = createdDebts.SingleOrDefault(e => e.OwnerUserId == transactionUserDebt.LenderUserId);
            if (endUserDebt == null) continue;

            var endUserDebtAmount = endUserDebt.Amount;
            var transactionUserDebtAmount = transactionUserDebt.Amount;

            endUserDebt.Amount -= transactionUserDebtAmount;
            endUserDebt.TotalAmount = endUserDebt.Amount;
            transactionUserDebt.Amount -= endUserDebtAmount;

            if (endUserDebt.Amount <= 0) createdDebts.Remove(endUserDebt);
            if (transactionUserDebt.Amount > 0) continue;
            
            transactionUserDebt.Amount = 0;
            transactionUserDebt.IsPaid = true;
        }

        await _context.SaveChangesAsync();

        var transaction = _mapper.Map<TransactionDto>(options);
        var transactionEntity = _mapper.Map<TransactionEntity>(transaction);
        transactionEntity.Debts = createdDebts;

        var addedTransaction = await _context.Transactions.AddAsync(transactionEntity);
        await _context.SaveChangesAsync();

        var addedTransactionDto = _mapper.Map<TransactionDto>(addedTransaction.Entity);
        return ResultTuple.Success(addedTransactionDto);
    }

    public async Task<ResultTuple> ResolveDebt(int userId, int debtId)
    {
        var foundDebt = await _context.Debts.SingleOrDefaultAsync(d => d.Id == debtId);
        if (foundDebt == null) return ResultTuple.Exception(StatusCodes.Status404NotFound, "Debt does not exist");

        if (foundDebt.OwnerUserId != userId) return ResultTuple.Exception(StatusCodes.Status403Forbidden, "User cannot resolve other users debts.");

        foundDebt.Amount = 0;
        foundDebt.IsPaid = true;

        await _context.SaveChangesAsync();

        var foundDebtDto = _mapper.Map<DebtDto>(foundDebt);
        return ResultTuple.Success(foundDebtDto);
    }
} 