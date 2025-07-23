using USplitAPI.Helpers;

namespace USplitAPI.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<ResultTuple> IsValidAsync(string token);

    Task<ResultTuple> RevalidateRefreshTokenByUserAsync(int userId);

    Task<ResultTuple> GenerateAsync(int userId);

    Task<ResultTuple> RefreshSessionAsync(string token);
}