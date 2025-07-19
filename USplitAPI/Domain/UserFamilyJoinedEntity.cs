namespace USplitAPI.Domain;

public class UserFamilyJoinedEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public Guid FamilyId { get; set; }
    public FamilyEntity Family { get; set; }
}