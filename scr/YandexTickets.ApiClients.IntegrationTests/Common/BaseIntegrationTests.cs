using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Common.Services;

namespace YandexTickets.ApiClients.IntegrationTests.Common;

/// <summary>
/// Базовый класс для интеграционных тестов.
/// </summary>
/// <typeparam name="TClient">Тип клиента API, используемого в тестах.</typeparam>
/// <typeparam name="TTestData">Тип данных тестирования, 
/// содержащих необходимые параметры и конфигурацию.</typeparam>
/// <typeparam name="TFixture">Тип фикстуры, 
/// предоставляющей зависимости и настройки для тестов.</typeparam>
public abstract class BaseIntegrationTests<TClient, TTestData, TFixture> : IClassFixture<TFixture>
	where TClient : class
	where TTestData : BaseTestData
	where TFixture : BaseFixture
{
	protected readonly TClient _client;
	protected readonly TTestData _testData;
	protected readonly IServiceProvider _serviceProvider;
	protected readonly string _auth;

	public BaseIntegrationTests(TFixture fixture)
	{
		_serviceProvider = fixture.ServiceProvider;
		_client = _serviceProvider.GetRequiredService<TClient>();
		var options = _serviceProvider.GetRequiredService<IOptions<TTestData>>();
		_testData = options.Value ?? throw new InvalidOperationException("Не создан файл настроек.");

		if (string.IsNullOrWhiteSpace(_testData.Login) || string.IsNullOrWhiteSpace(_testData.Password))
			throw new InvalidOperationException("В настройках не указан логин или пароль.");

		_auth = AuthService.GenerateAuthToken(_testData.Login, _testData.Password);
	}

	protected int GetCityId()
	{
		if (!_testData.CityId.HasValue)
			throw new InvalidOperationException("Не указан идентификатор города в настройках тестовых данных.");

		return _testData.CityId.Value;
	}

	protected static void AssertResponseSuccess<T>(ResponseBase<List<T>> response)
	{
		Assert.True(response.Status == ResponseStatus.Success, response.Error);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}
}
