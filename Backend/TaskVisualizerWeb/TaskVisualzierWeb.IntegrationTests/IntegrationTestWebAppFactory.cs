using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskVisualizerWeb.Repository;
using Testcontainers.PostgreSql;

namespace TaskVisualzierWeb.IntegrationTests;

public class IntegrationTestWebAppFactory
    : WebApplicationFactory<TaskVisualizerWeb.Presentation.Program>,
      IAsyncLifetime
{
    // "Host=localhost;Port=32321; Database=TaskVisualizerDB; Username=ps_user; Password=SecurePassword"
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("TaskVisualizerDB-IntegrationTest")
        .WithUsername("ps_user")
        .WithPassword("testpassword")
        .WithImage("postgres:14")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<EfCorePostgreContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<EfCorePostgreContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
