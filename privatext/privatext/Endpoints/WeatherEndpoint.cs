using FastEndpoints;
using privatext.DTO;
using privatext.Requests;

namespace privatext.Endpoints;

public class WeatherEndpoint : Endpoint<WeatherRequest, WeatherDTO[]>
{
    public override void Configure()
    {
        Get("forecasts/{amount}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(WeatherRequest r, CancellationToken c)
    {
        var list = new List<WeatherDTO>();
        for (int i = 1; i <= r.AmountToGet; i++)
        {
            list.Add(new()
            {
                Date = DateTime.Today.AddDays(i),
                Summary = $"i am {i}",
                TemperatureC = i + 34
            });
        }
        await SendAsync(list.ToArray());
    }
}
