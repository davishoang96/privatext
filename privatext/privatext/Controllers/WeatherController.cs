using Microsoft.AspNetCore.Mvc;
using privatext.DTO;

namespace privatext.Controllers;

public class WeatherController : RootControlller
{
    [HttpGet("GetWeatherData")]
    public IEnumerable<WeatherDTO> GetWeatherData()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        return Enumerable.Range(1, 1000).Select(index => new WeatherDTO
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        });
    }
}
