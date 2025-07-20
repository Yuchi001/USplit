namespace USplitAPI.Domain;

public class CurrentBalanceEntity
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public int UserId { get; set; }
    public int FamilyId { get; set; }
    
    public UserEntity User { get; set; }
    public FamilyEntity Family { get; set; }
}