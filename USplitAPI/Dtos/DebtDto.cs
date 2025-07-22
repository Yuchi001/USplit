namespace USplitAPI.Dtos;

public class DebtDto
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int LenderUserId { get; set; }
    public int OwnerUserId { get; set; }
    public int OwnerFamilyId { get; set; }
    public string Details { get; set; } = "";
    public DateTime CreateDate { get; set; }
    public UserShortDto LenderUser { get; set; }

    public class UserShortDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = "";
    }
}