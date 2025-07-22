namespace USplitAPI.Domain;

public class UserFamilyJoinedEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public int FamilyId { get; set; }
    public FamilyEntity Family { get; set; }
    
    public List<DebtEntity> Debts { get; set; }
}