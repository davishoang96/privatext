using System.Security.Cryptography;
using System.Text;

namespace privatext.Services;

public class RandomString : IRandomString
{
    public string CreateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:',.<>?/";
        StringBuilder result = new StringBuilder(length);
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[randomBytes[i] % chars.Length]);
            }
        }

        return result.ToString();
    }
}