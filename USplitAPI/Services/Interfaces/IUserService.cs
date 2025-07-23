using USplitAPI.Dtos;
using USplitAPI.Helpers;

namespace USplitAPI.Services.Interfaces;

public interface IUserService
{
    Task<ResultTuple> RemoveUserAsync(int id);
    Task<ResultTuple> AddUserAsync(UserDto userDto);
}