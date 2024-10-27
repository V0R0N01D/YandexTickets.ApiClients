using Microsoft.Extensions.DependencyInjection;

namespace YandexTickets.ApiClients.IntegrationTests.Common;

public abstract class BaseFixture : IDisposable
{
    protected readonly IServiceCollection services = new ServiceCollection();
    public IServiceProvider ServiceProvider { get; protected set; }

    public virtual void Dispose()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
