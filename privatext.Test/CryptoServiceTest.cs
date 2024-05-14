using FluentAssertions;
using privatext.Services;

namespace privatext.Test;

public class CryptoServiceTest
{
    private CryptoService cryptoService;
    public CryptoServiceTest()
    {
        cryptoService = new CryptoService();
    }

    [Fact]
    public async Task EncryptOK()
    {
        // Arrange
        var c = "Skyrim";

        // Act
        var result = await cryptoService.Encrypt(c);

        // Assert
        result.Should().Be("77CkamkU88DduRo1iPX0ng==");
    }

    [Fact]
    public async Task DecryptOK()
    {
        // Arrange
        var c = "77CkamkU88DduRo1iPX0ng==";

        // Act
        var result = await cryptoService.Decrypt(c);

        // Assert
        result.Should().Be("Skyrim");
    }
}
