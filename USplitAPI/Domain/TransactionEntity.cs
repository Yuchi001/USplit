namespace USplitAPI.Domain;

public class TransactionEntity
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public Guid UserId { get; set; }
    public Guid FamilyId { get; set; }
    
    public UserEntity User { get; set; }
    public FamilyEntity Family { get; set; }
}