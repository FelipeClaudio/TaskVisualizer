using Microsoft.EntityFrameworkCore;
using TaskVisualizerWeb.Application;
using TaskVisualizerWeb.Domain;
using TaskVisualizerWeb.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configValue = builder.Configuration.GetValue<string>("ConnectionStrings:WebApiDatabase");

builder.Services.AddDbContext<EfCorePostgreContext>(options => options.UseNpgsql(configValue));
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
