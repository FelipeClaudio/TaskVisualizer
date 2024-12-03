using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskVisualizerWeb.Application;
using TaskVisualizerWeb.Application.Task;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Domain;
using TaskVisualizerWeb.Domain.Models.Task;
using TaskVisualizerWeb.Domain.Models.User;
using TaskVisualizerWeb.Presentation.Middlewares;
using TaskVisualizerWeb.Repository;

namespace TaskVisualizerWeb.Presentation;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var configValue = builder.Configuration.GetValue<string>("ConnectionStrings:WebApiDatabase");

        builder.Services.AddDbContext<EfCorePostgreContext>(options => options.UseNpgsql(configValue));

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IValidator<User>, UserValidator>();

        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IValidator<Domain.Models.Task.Task>, TaskValidator>();

        builder.Services.AddScoped<IDateProvider, DateProvider>();

        var app = builder.Build();

        app.UseMiddleware<ExcpetionHandlerMiddleware>();

        app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}