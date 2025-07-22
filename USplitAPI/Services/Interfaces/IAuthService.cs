using USplitAPI.Dtos;

namespace USplitAPI.Services.Interfaces;

public interface IAuthService
{
    Task<ResultTuple> RegisterUserAsync(string email, string displayName, string password);
    Task<ResultTuple> LoginUserAsync(string email, string password, bool rememberMe);
    Task<ResultTuple> LogoutUserAsync(int userId);
    Task<ResultTuple> IsEmailTakenAsync(string email);
}