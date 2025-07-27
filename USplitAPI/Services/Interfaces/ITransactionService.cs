using USplitAPI.Dtos;
using USplitAPI.Helpers;

namespace USplitAPI.Services.Interfaces;

public interface ITransactionService
{
    Task<ResultTuple> GetUserDebtsAsync(int familyId, int userId);

    Task<ResultTuple> AddTransaction(TransactionOptionsDto options);
}