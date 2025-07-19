using USplitAPI.Dtos;

namespace USplitAPI.Domain;

public class FamilyEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    
    public List<UserFamilyJoinedEntity> UserFamilyList { get; set; } = new();
    public List<TransactionEntity> TransactionList { get; set; } = new();
}