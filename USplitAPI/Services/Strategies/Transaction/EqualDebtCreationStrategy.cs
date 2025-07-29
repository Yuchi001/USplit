using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;

namespace USplitAPI.Services.Strategies.Transaction;

public class EqualDebtCreationStrategy : IDebtCreationStrategy
{
    public ResultTuple CreateDebts(TransactionOptionsDto options)
    {
        var debts = new List<DebtEntity>();
        foreach (var participantId in options.ParticipantList)
        {
            var debtEntity = new DebtEntity
            {
                Amount = options.Amount / (options.ParticipantList.Count + 1),
                TotalAmount = options.Amount / (options.ParticipantList.Count + 1),
                LenderUserId = options.UserId,
                OwnerUserId = participantId,
                OwnerFamilyId = options.FamilyId,
                IsPaid = false,
            };
            debts.Add(debtEntity);
        }
        return ResultTuple.Success(debts);
    }
}