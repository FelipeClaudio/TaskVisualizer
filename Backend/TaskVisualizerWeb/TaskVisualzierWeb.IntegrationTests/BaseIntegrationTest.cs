using Microsoft.Extensions.DependencyInjection;
using TaskVisualizerWeb.Repository;
using TaskVisualzierWeb.IntegrationTests;

public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>,
      IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly EfCorePostgreContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider
            .GetRequiredService<EfCorePostgreContext>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}
