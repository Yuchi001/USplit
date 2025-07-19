using USplitAPI.Dtos;

namespace USplitAPI.Services.Interfaces;

public interface IUserService
{
    Task<ResultTuple> RegisterUserAsync(UserDto userDto);

    Task<ResultTuple> IsEmailTaken(string email);
}