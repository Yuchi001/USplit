using Microsoft.AspNetCore.Identity;

namespace USplitAPI.Helpers;

public static class PasswordHelper
{
    public static bool Verify(string password, string hashedPassword)
    {
        var hasher = new PasswordHasher<object>();
        return hasher.VerifyHashedPassword(null!, hashedPassword, password) == PasswordVerificationResult.Success;
    }

    public static string Hash(string password)
    {
        var hasher = new PasswordHasher<object>();
        return hasher.HashPassword(null!, password);
    }
}