namespace USplitAPI.Domain;

public class DebtEntity
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int TotalAmount { get; set; }
    public int LenderUserId { get; set; }
    public int TransactionId { get; set; }
    public int OwnerUserId { get; set; }
    public int OwnerFamilyId { get; set; }
    public bool IsPaid { get; set; }

    public UserEntity? LenderUser { get; set; }
    public TransactionEntity? Transaction { get; set; }
    public UserFamilyJoinedEntity? UserFamily { get; set; }
}