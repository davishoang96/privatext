using FastEndpoints;

namespace privatext.Common.Request;

public class GetWeatherRequest
{
    [QueryParam]
    public int AmountToGet { get; set; }
}