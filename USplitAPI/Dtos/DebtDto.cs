namespace USplitAPI.Dtos;

public class DebtDto
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int TotalAmount { get; set; }
    public int LenderUserId { get; set; }
    public int TransactionId { get; set; }
    public int OwnerUserId { get; set; }
    public int OwnerFamilyId { get; set; }
    public bool IsPaid { get; set; }
    
    public UserShortDto? LenderUser { get; set; }
    public TransactionShortDto? TransactionData { get; set; }

    public class UserShortDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = "";
    }

    public class TransactionShortDto
    {
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
    }
}