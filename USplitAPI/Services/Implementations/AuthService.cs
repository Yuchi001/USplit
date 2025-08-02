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
        if (isTaken.Result<bool>()) return ResultTuple.Exception(StatusCodes.Status403Forbidden, "Email is already taken.");

        var userToAdd = new UserDto
        {
            Email = email,
            DisplayName = displayName,
            Password = password,
            DateJoined = DateTime.UtcNow,
        };
        var createdUser = await _userService.AddUserAsync(userToAdd);

        return createdUser;
    }

    public async Task<ResultTuple> LoginUserAsync(string email, string password)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Email.ToLower() == email.ToLower());
        if (user == null) return ResultTuple.Exception(StatusCodes.Status404NotFound, "User does not exist.");

        if (!PasswordHelper.Verify(password, user.Password))
            return ResultTuple.Exception(StatusCodes.Status400BadRequest, "Password is not valid.");

        var tokenJWT = JWTHelper.GenerateJwtToken(user.Id, _configuration);
        
        var refreshToken = await _refreshTokenService.GenerateAsync(user.Id);
        
        return ResultTuple.Success(new { token = tokenJWT, refresh_token = refreshToken.Result<RefreshTokenDto>().Token });
    }
    
    public async Task<ResultTuple> IsEmailTakenAsync(string email)
    {
        var taken = await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        return ResultTuple.Success(taken);
    }

    public async Task<ResultTuple> RefreshSessionAsync(string refreshToken)
    {
        var token = await _refreshTokenService.RefreshSessionAsync(refreshToken);
        return token;
    }
}