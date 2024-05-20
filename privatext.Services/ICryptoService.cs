namespace privatext.Services;

public interface ICryptoService
{
    Task<string> Encrypt(string text, string key);
    Task<string> Decrypt(string secret, string key);
}
