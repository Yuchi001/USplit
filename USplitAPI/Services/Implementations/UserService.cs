using AutoMapper;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class UserService : IUserService
{
    private readonly USplitDBContext _context;
    private readonly IMapper _mapper;

    public UserService(USplitDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResultTuple> RemoveUserAsync(int id)
    {
        var userToDelete = await _context.Users.SingleOrDefaultAsync(e => e.Id == id);
        if (userToDelete == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);

        var deletedUser = _context.Users.Remove(userToDelete);

        await _context.SaveChangesAsync();

        var deletedUserDto = _mapper.Map<UserDto>(deletedUser.Entity);
        return ResultTuple.Success(deletedUserDto);
    }

    public async Task<ResultTuple> AddUserAsync(UserDto userDto)
    {
        userDto.Password = PasswordHelper.Hash(userDto.Password);
        var userEntity = _mapper.Map<UserEntity>(userDto);
        var addedUser = await _context.Users.AddAsync(userEntity);

        await _context.SaveChangesAsync();

        var addedUserDto = _mapper.Map<UserDto>(addedUser.Entity);
        
        return ResultTuple.Success(addedUserDto);
    }
}