using USplitAPI.Dtos;

namespace USplitAPI.Services.Interfaces;

public interface IUserService
{
    Task<ResultTuple> RemoveUserAsync(int id);
    Task<ResultTuple> RegisterUserAsync(UserDto userDto);
    Task<ResultTuple> LoginUserAsync(string email, string password, bool rememberMe);
    Task<ResultTuple> LogoutUserAsync(int userId);
    Task<ResultTuple> IsEmailTakenAsync(string email);
}