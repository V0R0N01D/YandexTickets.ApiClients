using YandexTickets.CrmApiClient;
using YandexTickets.Common.Models.Enums;
using YandexTickets.CrmApiClient.Models.Requests;
using Microsoft.Extensions.DependencyInjection;
using YandexTickets.IntegrationTests.Models;
using Microsoft.Extensions.Options;

namespace YandexTickets.IntegrationTests.YandexTicketsCrmClientTests;

/// <summary>
/// Интеграционные тесты для методов YandexTicketCrmClient.
/// </summary>
public class YandexTicketCrmClientIntegrationTests : IClassFixture<TestFixture>
{
	private readonly IYandexTicketsCrmApiClient _client;
	private readonly CrmTestData _crmTestData;

	public YandexTicketCrmClientIntegrationTests(TestFixture fixture)
	{
		_client = fixture.ServiceProvider.GetRequiredService<IYandexTicketsCrmApiClient>();
		var option = fixture.ServiceProvider.GetRequiredService<IOptions<CrmTestData>>();
		_crmTestData = option.Value;

		if (_crmTestData is null || string.IsNullOrWhiteSpace(_crmTestData.AuthToken))
			Assert.Fail("Не указан токен авторизации.");
	}

	/// <summary>
	/// Проверяет получение списка городов.
	/// </summary>
	[Fact]
	public async Task GetCityListAsync()
	{
		var request = new GetCityListRequest(_crmTestData.AuthToken);
		var response = await _client.GetCityListAsync(request);

		Assert.Equal(ResponseStatus.Success, response.Status);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}

	/// <summary>
	/// Проверяет получение списка мероприятий для указанного города.
	/// </summary>
	[Fact]
	public async Task GetActivityListAsync()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			Assert.Fail("Не указан идентификатор города.");

		var request = new GetActivityListRequest(_crmTestData.AuthToken, _crmTestData.CityId);
		var response = await _client.GetActivityListAsync(request);

		Assert.Equal(ResponseStatus.Success, response.Status);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}

	/// <summary>
	/// Проверяет получение списка событий для указанного города.
	/// </summary>
	[Fact]
	public async Task GetEventListAsync()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			Assert.Fail("Не указан идентификатор города.");

		var request = new GetEventListRequest(_crmTestData.AuthToken, _crmTestData.CityId);
		var response = await _client.GetEventListAsync(request);

		Assert.Equal(ResponseStatus.Success, response.Status);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}
}
