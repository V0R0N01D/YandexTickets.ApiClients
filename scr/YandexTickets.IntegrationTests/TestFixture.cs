using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YandexTickets.CrmApiClient;
using YandexTickets.IntegrationTests.Models;

namespace YandexTickets.IntegrationTests;

/// <summary>
/// Фикстура для настройки сервисов и зависимостей в интеграционных тестах.
/// </summary>
public class TestFixture : IDisposable
{
	public IServiceProvider ServiceProvider { get; private set; }

	/// <summary>
	/// Конструктор, инициализирующий контейнер зависимостей для тестов.
	/// </summary>
	public TestFixture()
	{
		var services = new ServiceCollection();

		var configuration = new ConfigurationBuilder().AddJsonFile("crm_settings.json").Build();
		services.AddSingleton<IConfiguration>(configuration);
		services.Configure<CrmTestData>(configuration.GetSection("CrmTestData"));

		services.AddHttpClient<YandexTicketsCrmApiClient>();

		ServiceProvider = services.BuildServiceProvider();
	}

	/// <summary>
	/// Освобождает ресурсы, занятые контейнером зависимостей.
	/// </summary>
	public void Dispose()
	{
		if (ServiceProvider is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
}
