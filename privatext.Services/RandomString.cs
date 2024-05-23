using PasswordGenerator;

namespace privatext.Services;

public class RandomString : IRandomString
{
    private readonly Password password;

    public RandomString()
    {
        password = new Password();
    }

    public string Generate(int length)
    {
        return password.LengthRequired(length)
            .IncludeLowercase()
            .IncludeNumeric().IncludeUppercase().Next();
    }
}