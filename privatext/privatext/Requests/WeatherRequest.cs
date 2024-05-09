using FastEndpoints;

namespace privatext.Requests;

public class WeatherRequest
{
    [BindFrom("amount")]
    public int AmountToGet { get; set; }
}
