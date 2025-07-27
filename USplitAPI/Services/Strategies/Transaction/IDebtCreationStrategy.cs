using USplitAPI.Dtos;
using USplitAPI.Helpers;

namespace USplitAPI.Services.Strategies.Transaction;

public interface IDebtCreationStrategy
{
    ResultTuple CreateDebts(TransactionOptionsDto options);
}