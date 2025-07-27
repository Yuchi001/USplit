namespace USplitAPI.Domain;

public class TransactionEntity
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int OwnerFamilyId { get; set; }
    public string Details { get; set; } = "";
    public int OwnerUserId { get; set; }
    public string SplitType { get; set; } = "";
    
    public UserFamilyJoinedEntity UserFamily { get; set; }
    public List<DebtEntity> Debts { get; set; } = new();
}