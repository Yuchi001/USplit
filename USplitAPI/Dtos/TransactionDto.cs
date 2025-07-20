namespace USplitAPI.Dtos;

public class TransactionDto
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int UserId { get; set; }
    public int FamilyId { get; set; }
}