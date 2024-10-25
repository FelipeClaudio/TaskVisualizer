namespace TaskVisualizerWeb.Domain;

public interface IWeatherForecastRepository
{
    public List<WeatherForecast> Get();
}