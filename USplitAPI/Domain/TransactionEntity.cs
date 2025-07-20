namespace USplitAPI.Domain;

public class TransactionEntity
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int UserId { get; set; }
    public int FamilyId { get; set; }
    
    public UserEntity User { get; set; }
    public FamilyEntity Family { get; set; }
}