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
        var k = "4bco&tHHYGn&LZF&BtAN$8wG^f%8Ag&9";

        // Act
        var result = await cryptoService.Encrypt(c, k);

        // Assert
        result.Should().Be("XpM8Orl4xf5J0gDE7/Zm9A==");
    }

    [Fact]
    public async Task DecryptOK()
    {
        // Arrange
        var c = "XpM8Orl4xf5J0gDE7/Zm9A==";
        var k = "4bco&tHHYGn&LZF&BtAN$8wG^f%8Ag&9";

        // Act
        var result = await cryptoService.Decrypt(c, k);

        // Assert
        result.Should().Be("Skyrim");
    }
}
