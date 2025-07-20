namespace USplitAPI.Domain;

public class RefreshTokenEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Token { get; set; } = "";
    
    public UserEntity User { get; set; }
}