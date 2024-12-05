using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Data.Common;
using TaskVisualizerWeb.Repository;
using Testcontainers.PostgreSql;

namespace TaskVisualzierWeb.IntegrationTests;

public class IntegrationTestWebAppFactory
    : WebApplicationFactory<TaskVisualizerWeb.Presentation.Program>,
      IAsyncLifetime
{
    private Respawner _respawner = null;
    private DbConnection _connection = null!;
    public EfCorePostgreContext DbContext { get; private set; } = null!;


    // "Host=localhost;Port=32321; Database=TaskVisualizerDB; Username=ps_user; Password=SecurePassword"
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("TaskVisualizerDB-IntegrationTest")
        .WithUsername("ps_user")
        .WithPassword("testpassword")
        .WithImage("postgres:14")
        .Build();

    public async Task ResetDatabase()
    {
        await _respawner.ResetAsync(_connection);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        DbContext = Services.CreateScope().ServiceProvider.GetRequiredService<EfCorePostgreContext>();
        _connection = DbContext.Database.GetDbConnection();
        await _connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }

    public new async Task DisposeAsync()
    {
        await _connection.CloseAsync();
        await _dbContainer.DisposeAsync();
    }



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
}
