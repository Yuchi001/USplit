using System.Security.Cryptography;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class RefreshTokenService : IRefreshTokenService
{
    private const int EXPIRY_DAYS = 7;
    
    private readonly USplitDBContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public RefreshTokenService(USplitDBContext context, IMapper mapper, IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ResultTuple> IsValidAsync(string token)
    {
        var tokenEntity = await _context.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token);
        return ResultTuple.Success(tokenEntity != null && tokenEntity.ExpiryDate > DateTime.UtcNow);
    }

    public async Task<ResultTuple> GenerateAsync(int userId)
    {
        var revalidateTuple = await RevalidateRefreshTokenByUserAsync(userId);
        if (revalidateTuple.result != null) return revalidateTuple;
        
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var tokenToAdd = new RefreshTokenEntity
        {
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddDays(EXPIRY_DAYS),
            Token = Convert.ToBase64String(randomBytes),
        };

        var addedToken = await _context.RefreshTokens.AddAsync(tokenToAdd);
        await _context.SaveChangesAsync();

        var addedTokenDto = _mapper.Map<RefreshTokenDto>(addedToken.Entity);
        return ResultTuple.Success(addedTokenDto);
    }

    public async Task<ResultTuple> RevalidateRefreshTokenByUserAsync(int userId)
    {
        var token = await _context.RefreshTokens.SingleOrDefaultAsync(e => e.UserId == userId);
        if (token == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);
        
        token.ExpiryDate = DateTime.UtcNow.AddDays(EXPIRY_DAYS);

        await _context.SaveChangesAsync();

        var updatedToken = _mapper.Map<RefreshTokenDto>(token);
        return ResultTuple.Success(updatedToken);
    }
    
    public async Task<ResultTuple> RefreshSessionAsync(string token)
    {
        var foundToken = await _context.RefreshTokens.AsNoTracking().SingleOrDefaultAsync(e => e.Token == token);
        if (foundToken == null || foundToken.ExpiryDate < DateTime.UtcNow)
            return ResultTuple.Exception(StatusCodes.Status403Forbidden);

        var revalidateTuple = await RevalidateRefreshTokenByUserAsync(foundToken.UserId);
        var newToken = JWTHelper.GenerateJwtToken(foundToken.UserId, _configuration);

        return ResultTuple.Success(new { token = newToken, refresh_token = revalidateTuple.result });
    }
}