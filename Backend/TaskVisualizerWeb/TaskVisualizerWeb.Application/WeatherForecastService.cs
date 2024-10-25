using TaskVisualizerWeb.Domain;

namespace TaskVisualizerWeb.Application;

public class WeatherForecastService(IWeatherForecastRepository repository) : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _repository = repository;

    public List<WeatherForecast> GetWeatherForecasts() => _repository.Get();
}
