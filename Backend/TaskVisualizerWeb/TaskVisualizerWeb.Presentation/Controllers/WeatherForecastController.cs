using Microsoft.AspNetCore.Mvc;
using TaskVisualizerWeb.Application;
using TaskVisualizerWeb.Domain;

namespace TaskVisualizerWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger = logger;
        private readonly IWeatherForecastService _weatherForecastService = weatherForecastService;

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get() => _weatherForecastService.GetWeatherForecasts();
    }
}
