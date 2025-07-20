namespace USplitAPI.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime DateJoined { get; set; }
}