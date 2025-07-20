namespace USplitAPI.Domain;

public class UserEntity
{
    public Guid Id { get; set; }
    public string AuthId { get; set; } = "";
    public string Email { get; set; } = "";
    public string DisplayName { get; set; } = "";

    public List<UserFamilyJoinedEntity> UserFamilyList { get; set; } = [];
    public List<TransactionEntity> TransactionList { get; set; } = [];
    public List<CurrentBalanceEntity> CurrentBalanceList { get; set; } = [];
}