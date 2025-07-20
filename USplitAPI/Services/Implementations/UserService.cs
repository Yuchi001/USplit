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
    
    public async Task<ResultTuple> RegisterUserAsync(UserDto userDto)
    {
        var userEntity = _mapper.Map<UserEntity>(userDto);
        var addedUser = await _context.Users.AddAsync(userEntity);

        await _context.SaveChangesAsync();

        var addedUserDto = _mapper.Map<UserDto>(addedUser.Entity);
        
        return ResultTuple.Success(addedUserDto);
    }

    public async Task<ResultTuple> LoginUserAsync(string email, string password, bool rememberMe)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(e =>
            e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);

        if (!PasswordHelper.Verify(password, user.Password))
            return ResultTuple.Exception(StatusCodes.Status401Unauthorized);

        var tokenJWT = JWTHelper.GenerateJwtToken(user.Id, _configuration);
        if (!rememberMe) return ResultTuple.Success(new { token = tokenJWT });
        
        var refreshToken = await _refreshTokenService.GenerateAsync(user.Id);
        
        return ResultTuple.Success(new { token = tokenJWT, refresh_token = refreshToken });
    }

    public async Task<ResultTuple> IsEmailTakenAsync(string email)
    {
        var taken = await _context.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return ResultTuple.Success(taken);
    }
}