using USplitAPI.Dtos;

namespace USplitAPI.Domain;

public class FamilyEntity
{
    public int Id { get; set; }
    public int OwnerUserId { get; set; }
    public string Name { get; set; } = "";
    
    public UserEntity User { get; set; }
    public List<UserFamilyJoinedEntity> UserFamilyList { get; set; } = new();
    public List<TransactionEntity> TransactionList { get; set; } = new();
}