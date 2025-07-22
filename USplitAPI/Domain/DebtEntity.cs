namespace USplitAPI.Domain;

public class DebtEntity
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int LenderUserId { get; set; }
    public int OwnerUserId { get; set; }
    public int OwnerFamilyId { get; set; }
    public string Details { get; set; } = "";
    public DateTime CreateDate { get; set; }
    
    public UserEntity LenderUser { get; set; }
    public UserFamilyJoinedEntity UserFamily { get; set; }
}