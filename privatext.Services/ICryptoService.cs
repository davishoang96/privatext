namespace privatext.Services;

public interface ICryptoService
{
    Task<string> Encrypt(string text);
    Task<string> Decrypt(string secret);
}
