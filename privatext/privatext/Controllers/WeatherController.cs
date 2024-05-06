using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using privatext.Shared;

namespace privatext.Controllers;

public class WeatherController : PrivaBaseController
{
    [Authorize]
    [HttpGet("GetWeatherData")]
    public async Task<IEnumerable<WeatherDTO>> GetWeatherDTOs()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        return await Task.Run(() =>
        {
            return Enumerable.Range(1, 1000).Select(index => new WeatherDTO
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            });
        });
    }
}
