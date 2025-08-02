namespace USplitAPI.Helpers;

public static class UserCodeHelper
{
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public static string GenerateCode(int userId, int randomLength = 4)
    {
        var userIdBase32 = EncodeBase32(userId);
        var randomPart = GenerateRandomString(randomLength);
        return userIdBase32 + randomPart;
    }

    private static string GenerateRandomString(int length)
    {
        var random = new Random();
        return new string(Enumerable.Range(0, length)
            .Select(_ => Alphabet[random.Next(Alphabet.Length)]).ToArray());
    }

    private static string EncodeBase32(int value)
    {
        if (value == 0) return Alphabet[0].ToString();

        var result = "";
        var baseSize = Alphabet.Length;

        while (value > 0)
        {
            int remainder = value % baseSize;
            value /= baseSize;
            result = Alphabet[remainder] + result;
        }

        return result;
    }
}