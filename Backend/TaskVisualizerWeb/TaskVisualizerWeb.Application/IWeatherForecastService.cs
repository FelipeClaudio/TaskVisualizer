using TaskVisualizerWeb.Domain;

namespace TaskVisualizerWeb.Application
{
    public interface IWeatherForecastService
    {
        List<WeatherForecast> GetWeatherForecasts();
    }
}