using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;

namespace USplitAPI.Services.Strategies.Transaction;

public class DetailedDebtCreationStrategy : IDebtCreationStrategy
{
    public ResultTuple CreateDebts(TransactionOptionsDto options)
    {
        var sum = 0;
        var debts = new List<DebtEntity>();
        foreach (var participant in options.ParticipantDetailedList)
        {
            sum += participant.Amount;
            var debtEntity = new DebtEntity
            {
                Amount = participant.Amount,
                TotalAmount = participant.Amount,
                LenderUserId = options.UserId,
                OwnerUserId = participant.UserId,
                OwnerFamilyId = options.FamilyId,
                IsPaid = false,
            };
            debts.Add(debtEntity);
        }

        if (sum < options.Amount) return ResultTuple.Success(debts);
        
        return ResultTuple.Exception(StatusCodes.Status403Forbidden, "Summed debts are bigger than transaction value.");
    }
}