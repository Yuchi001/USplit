namespace USplitAPI.Dtos;

public class RefreshTokenDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Token { get; set; } = "";
}