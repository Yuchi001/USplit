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
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenService _refreshTokenService;
    

    public UserService(USplitDBContext context, IMapper mapper, IConfiguration configuration, IRefreshTokenService refreshTokenService)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
        _refreshTokenService = refreshTokenService;
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

    public async Task<ResultTuple> RegisterUserAsync(UserDto userDto)
    {
        userDto.Password = PasswordHelper.Hash(userDto.Password);
        var userEntity = _mapper.Map<UserEntity>(userDto);
        var addedUser = await _context.Users.AddAsync(userEntity);

        await _context.SaveChangesAsync();

        var addedUserDto = _mapper.Map<UserDto>(addedUser.Entity);
        
        return ResultTuple.Success(addedUserDto);
    }

    public async Task<ResultTuple> LoginUserAsync(string email, string password, bool rememberMe)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Email.ToLower() == email.ToLower());
        if (user == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);

        if (!PasswordHelper.Verify(password, user.Password))
            return ResultTuple.Exception(StatusCodes.Status401Unauthorized);

        var tokenJWT = JWTHelper.GenerateJwtToken(user.Id, _configuration);
        if (!rememberMe) return ResultTuple.Success(new { token = tokenJWT });
        
        var refreshToken = await _refreshTokenService.GenerateAsync(user.Id);
        
        return ResultTuple.Success(new { token = tokenJWT, refresh_token = refreshToken.Result<RefreshTokenDto>().Token });
    }

    public async Task<ResultTuple> LogoutUserAsync(int userId)
    {
        var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(e => e.UserId == userId);
        if (refreshToken == null) return ResultTuple.Success(true);

        _context.RefreshTokens.Remove(refreshToken);

        await _context.SaveChangesAsync();
        
        return ResultTuple.Success(true);
    }

    public async Task<ResultTuple> IsEmailTakenAsync(string email)
    {
        var taken = await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        return ResultTuple.Success(taken);
    }
}