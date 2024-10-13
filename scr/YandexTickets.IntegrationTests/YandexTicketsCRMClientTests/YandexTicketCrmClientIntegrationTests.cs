using YandexTickets.CrmApiClient;
using YandexTickets.Common.Models.Enums;
using YandexTickets.CrmApiClient.Models.Requests;
using Microsoft.Extensions.DependencyInjection;
using YandexTickets.IntegrationTests.Models;
using Microsoft.Extensions.Options;
using YandexTickets.Common.Models.Responses;
using YandexTickets.Common.Services;

namespace YandexTickets.IntegrationTests.YandexTicketsCrmClientTests;

/// <summary>
/// Интеграционные тесты для методов YandexTicketCrmClient.
/// </summary>
public class YandexTicketCrmClientIntegrationTests : IClassFixture<TestFixture>
{
	private readonly IYandexTicketsCrmApiClient _client;
	private readonly CrmTestData _crmTestData;
	private readonly string _auth;

	public YandexTicketCrmClientIntegrationTests(TestFixture fixture)
	{
		_client = fixture.ServiceProvider.GetRequiredService<IYandexTicketsCrmApiClient>();
		var option = fixture.ServiceProvider.GetRequiredService<IOptions<CrmTestData>>();
		_crmTestData = option.Value;

		if (_crmTestData is null)
			Assert.Fail("Не создан файл настроек.");

		if (string.IsNullOrWhiteSpace(_crmTestData.Login) || string.IsNullOrWhiteSpace(_crmTestData.Password))
			Assert.Fail("В настройках не указан логин или пароль.");

		_auth = AuthService.GenerateAuthToken(_crmTestData.Login, _crmTestData.Password);
	}

	/// <summary>
	/// Проверяет получение списка городов.
	/// </summary>
	[Fact]
	public async Task GetCityListAsync()
	{
		var request = new GetCityListRequest(_auth);
		var response = await _client.GetCityListAsync(request);

		AssertResponseListSuccess(response);
	}

	/// <summary>
	/// Проверяет получение списка мероприятий для указанного города.
	/// </summary>
	[Fact]
	public async Task GetActivityListAsync()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			Assert.Fail("Не указан идентификатор города.");

		var request = new GetActivityListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetActivityListAsync(request);

		AssertResponseListSuccess(response);
	}

	/// <summary>
	/// Проверяет получение списка событий для указанного города.
	/// </summary>
	[Fact]
	public async Task GetEventListAsync()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			Assert.Fail("Не указан идентификатор города.");

		var request = new GetEventListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetEventListAsync(request);

		AssertResponseListSuccess(response);
	}

	/// <summary>
	/// Проверяет получение отчета по событиям.
	/// </summary>
	[Fact]
	public async Task GetEventReportAsync()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			Assert.Fail("Не указан идентификатор города.");

		var request = new GetEventReportRequest(_auth, _crmTestData.CityId, _crmTestData.EventsId);
		var response = await _client.GetEventReportAsync(request);

		AssertResponseListSuccess(response);
	}


	// Вспомогательный метод для общих проверок
	private void AssertResponseListSuccess<T>(ResponseBase<List<T>> response)
	{
		Assert.True(response.Status == ResponseStatus.Success, response.Error);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}
}
