using AutoMapper;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly USplitDBContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserService _userService;
    
    public AuthService(USplitDBContext context, IMapper mapper, IConfiguration configuration, IRefreshTokenService refreshTokenService, IUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
        _refreshTokenService = refreshTokenService;
        _userService = userService;
    }
    
    public async Task<ResultTuple> RegisterUserAsync(string email, string displayName, string password)
    {
        var isTaken = await IsEmailTakenAsync(email);
        if (isTaken.Result<bool>()) return ResultTuple.Exception(StatusCodes.Status403Forbidden);

        var userToAdd = new UserDto
        {
            Email = email,
            DisplayName = displayName,
            Password = password,
        };
        var createdUser = await _userService.AddUserAsync(userToAdd);

        return ResultTuple.Success(createdUser);
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