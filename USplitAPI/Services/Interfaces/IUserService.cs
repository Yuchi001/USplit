using USplitAPI.Dtos;

namespace USplitAPI.Services.Interfaces;

public interface IUserService
{
    Task<ResultTuple> RegisterUserAsync(UserDto userDto);
    Task<ResultTuple> LoginUserAsync(string email, string password, bool rememberMe);

    Task<ResultTuple> IsEmailTakenAsync(string email);
}