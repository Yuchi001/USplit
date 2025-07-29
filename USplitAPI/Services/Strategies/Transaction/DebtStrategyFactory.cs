namespace USplitAPI.Services.Strategies.Transaction;

public class DebtStrategyFactory
{
    public IDebtCreationStrategy? GetDebtCreationStrategy(string rawSplitType)
    {
        var splitType = rawSplitType.Trim().ToLower();
        return splitType switch
        {
            "equal" => new EqualDebtCreationStrategy(),
            "detailed" => new DetailedDebtCreationStrategy(),
            _ => null
        };
    }
}