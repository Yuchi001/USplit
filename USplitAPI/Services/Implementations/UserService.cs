using AutoMapper;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class UserService(USplitDBContext context, IMapper mapper) : IUserService
{
    public async Task<ResultTuple> RegisterUserAsync(UserDto userDto)
    {
        var userEntity = mapper.Map<UserEntity>(userDto);
        var addedUser = await context.Users.AddAsync(userEntity);

        await context.SaveChangesAsync();

        var addedUserDto = mapper.Map<UserDto>(addedUser);
        
        return ResultTuple.Success(addedUserDto);
    }

    public async Task<ResultTuple> IsEmailTaken(string email)
    {
        var taken = await context.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return ResultTuple.Success(taken);
    }
}