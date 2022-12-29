using Microsoft.AspNetCore.Mvc;
using parcelfy.Application.WeatherForecasts.Abstractions;
using parcelfy.Application.WeatherForecasts.Models;

namespace parcelfy.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IGetWeatherForecastQuery _getWeatherForecastsQuery;

    public WeatherForecastController(IGetWeatherForecastQuery getWeatherForecastsQuery)
    {
        _getWeatherForecastsQuery = getWeatherForecastsQuery ?? throw new ArgumentNullException(nameof(getWeatherForecastsQuery));
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return  _getWeatherForecastsQuery.Get();
    }
}
