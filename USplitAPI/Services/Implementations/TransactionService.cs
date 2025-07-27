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

    public TransactionService(IMapper mapper, USplitDBContext context, DebtStrategyFactory debtStrategyFactory)
    {
        _mapper = mapper;
        _context = context;
        _debtStrategyFactory = debtStrategyFactory;
    }
    
    public async Task<ResultTuple> GetUserDebtsAsync(int familyId, int userId)
    {
        var foundFamily = await _context.UserFamilies.SingleOrDefaultAsync(e => e.FamilyId == familyId && e.UserId == userId);
        if (foundFamily == null) return ResultTuple.Exception(StatusCodes.Status404NotFound, "Family does not exist.");
        
        var foundDebts = await _context.Debts
            .Where(d => d.UserFamily.UserId == userId)
            .ProjectTo<DebtDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return ResultTuple.Success(foundDebts);
    }

    public async Task<ResultTuple> AddTransaction(TransactionOptionsDto options)
    {
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

    public async Task<ResultTuple> ResolveDebts(int lenderUserId, int ownerUserId, int amount)
    {
        var activeDebts = await _context.Debts
                .Where(e => !e.IsPaid && e.LenderUserId == lenderUserId && e.OwnerUserId == ownerUserId)
                .ToListAsync();
        if (!activeDebts.Any()) return ResultTuple.Success(amount);

        foreach (var debt in activeDebts)
        {
            amount -= debt.Amount;
            debt.IsPaid = true;
            if (amount > 0) continue;
            
            if (amount == 0) break;

            debt.Amount = (int)MathF.Abs(amount);
            debt.IsPaid = false;
            break;
        }
        
        await _context.SaveChangesAsync();
        
        return ResultTuple.Success(MathF.Min(0, amount));
    }
}