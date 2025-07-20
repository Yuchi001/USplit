namespace USplitAPI.Domain;

public class UserEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime DateJoined { get; set; }

    public List<UserFamilyJoinedEntity> UserFamilyList { get; set; } = new();
    public List<TransactionEntity> TransactionList { get; set; } = new();
    public List<CurrentBalanceEntity> CurrentBalanceList { get; set; } = new();
}