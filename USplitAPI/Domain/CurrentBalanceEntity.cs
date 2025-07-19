namespace USplitAPI.Domain;

public class CurrentBalanceEntity
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid UserId { get; set; }
    public Guid FamilyId { get; set; }
    
    public UserEntity User { get; set; }
    public FamilyEntity Family { get; set; }
}