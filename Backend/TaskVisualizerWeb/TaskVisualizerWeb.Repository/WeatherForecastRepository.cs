using TaskVisualizerWeb.Domain;

namespace TaskVisualizerWeb.Repository;

public class WeatherForecastRepository(EfCorePostgreContext context) : IWeatherForecastRepository
{
    private readonly EfCorePostgreContext _dbContext = context;

    public List<WeatherForecast> Get() => [.. _dbContext.WeatherForecasts];
}
