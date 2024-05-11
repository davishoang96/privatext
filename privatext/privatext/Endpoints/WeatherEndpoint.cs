using FastEndpoints;
using privatext.Common.Request;

namespace privatext.Endpoints;

public class WeatherEndpoint : Endpoint<GetWeatherRequest, ICollection<WeatherDTO>>
{
    public override void Configure()
    {
        Get("/forecasts/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetWeatherRequest r, CancellationToken c)
    {
        var list = new List<WeatherDTO>();
        for (int i = 1; i <= r.AmountToGet; i++)
        {
            list.Add(new WeatherDTO
            {
                Date = DateTime.Today.AddDays(i),
                Summary = $"i am {i}",
                TemperatureC = i + 34
            });
        }
        await SendAsync(list);
    }
}
