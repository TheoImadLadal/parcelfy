using parcelfy.Application.WeatherForecasts.Models;

namespace parcelfy.Application.WeatherForecasts.Abstractions;

public interface IGetWeatherForecastQuery
{
    IEnumerable<WeatherForecast> Get();
}
