namespace USplitAPI.Dtos;

public class CurrentBalanceDto
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid UserId { get; set; }
    public Guid FamilyId { get; set; }
}