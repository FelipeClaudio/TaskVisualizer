using Microsoft.Extensions.DependencyInjection;
using TaskVisualizerWeb.Repository;
using TaskVisualzierWeb.IntegrationTests;

public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>,
      IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly Func<Task> _resetDatabase;
    protected readonly EfCorePostgreContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _resetDatabase = factory.ResetDatabase;

        DbContext = _scope.ServiceProvider
            .GetRequiredService<EfCorePostgreContext>();
    }

    public async Task DisposeAsync()
    {
        await _resetDatabase();
        _scope?.Dispose();
        await DbContext.DisposeAsync();
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
