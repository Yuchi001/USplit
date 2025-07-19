namespace USplitAPI.Dtos;

public class TransactionDto
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public Guid UserId { get; set; }
    public Guid FamilyId { get; set; }
}