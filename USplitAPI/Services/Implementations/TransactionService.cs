using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly USplitDBContext _context;

    public TransactionService(IMapper mapper, USplitDBContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ResultTuple> GetUserDebtsAsync(int familyId, int userId)
    {
        var foundFamily = await _context.UserFamilies.SingleOrDefaultAsync(e => e.FamilyId == familyId && e.UserId == userId);
        if (foundFamily == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);
        
        var foundDebts = await _context.Debts
            .Where(d => d.UserFamily.UserId == userId)
            .ProjectTo<DebtDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return ResultTuple.Success(foundDebts);
    }

    public async Task<ResultTuple> AddTransaction(TransactionDto transaction)
    {
        var splitType = transaction.SplitType.Trim().ToLowerInvariant();
        var debts = new  List<DebtEntity>();
        if (splitType.Equals("equal"))
        {
            foreach (var participantId in transaction.ParticipantList)
            {
                var debtEntity = new DebtEntity
                {
                    Amount = transaction.Amount / (transaction.ParticipantList.Count + 1),
                    LenderUserId = transaction.OwnerUserId,
                    TransactionId = transaction.Id,
                    OwnerUserId = participantId,
                    OwnerFamilyId = transaction.FamilyId,
                    Details = transaction.Details,
                    CreateDate = DateTime.Now,
                };
                debts.Add(debtEntity);
            }
        }
        if (splitType.Equals("detailed"))
        {
            var sum = 0;
            foreach (var participant in transaction.ParticipantDetailedList)
            {
                sum += participant.Amount;
                var debtEntity = new DebtEntity
                {
                    Amount = participant.Amount,
                    LenderUserId = transaction.OwnerUserId,
                    TransactionId = transaction.Id,
                    OwnerUserId = participant.UserId,
                    OwnerFamilyId = transaction.FamilyId,
                    Details = transaction.Details,
                    CreateDate = DateTime.Now,
                };
                debts.Add(debtEntity);
            }

            if (sum > transaction.Amount) return ResultTuple.Exception(StatusCodes.Status403Forbidden, "Summed debts are bigger than transaction value.");
        }
        if (!debts.Any()) return ResultTuple.Exception(StatusCodes.Status400BadRequest, "Split type not valid.");

        var transactionEntity = _mapper.Map<TransactionEntity>(transaction);
        transactionEntity.Debts = debts;

        var addedTransaction = await _context.Transactions.AddAsync(transactionEntity);
        await _context.SaveChangesAsync();

        var addedTransactionDto = _mapper.Map<TransactionDto>(addedTransaction.Entity);
        return ResultTuple.Success(addedTransactionDto);
    }
}