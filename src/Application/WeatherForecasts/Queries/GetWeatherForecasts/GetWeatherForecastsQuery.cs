using parcelfy.Application.WeatherForecasts.Abstractions;
using parcelfy.Application.WeatherForecasts.Models;

namespace parcelfy.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public class GetWeatherForecastsQuery : IGetWeatherForecastQuery
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public IEnumerable<WeatherForecast> Get()
    {
        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return result;
    }
}
