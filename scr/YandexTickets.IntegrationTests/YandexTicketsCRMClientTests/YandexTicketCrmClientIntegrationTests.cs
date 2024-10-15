using YandexTickets.CrmApiClient;
using YandexTickets.Common.Models.Enums;
using YandexTickets.CrmApiClient.Models.Requests;
using Microsoft.Extensions.DependencyInjection;
using YandexTickets.IntegrationTests.Models;
using Microsoft.Extensions.Options;
using YandexTickets.Common.Models.Responses;
using YandexTickets.Common.Services;
using YandexTickets.CrmApiClient.Models.Responses;

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
	/// Проверяет получение списка мероприятий.
	/// </summary>
	[Fact]
	public async Task GetActivityListAsync()
	{
		CheckCityIsNotNull();

		var request = new GetActivityListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetActivityListAsync(request);

		AssertResponseListSuccess(response);
	}

	/// <summary>
	/// Проверяет получение списка событий.
	/// </summary>
	[Fact]
	public async Task GetEventListAsync()
	{
		CheckCityIsNotNull();

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
		CheckCityIsNotNull();

		var request = new GetEventReportRequest(_auth, _crmTestData.CityId, _crmTestData.EventsId);
		var response = await _client.GetEventReportAsync(request);

		AssertResponseListSuccess(response);
	}

	/// <summary>
	/// Проверяет получение списка заказов.
	/// </summary>
	[Fact]
	public async Task GetOrderListAsync()
	{
		CheckCityIsNotNull();

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseListSuccess(response);
	}

	/// <summary>
	/// Проверяет получение списка заказов c периодом дат.
	/// </summary>
	[Fact]
	public async Task GetOrderListWithDatesParamAsync()
	{
		CheckCityIsNotNull();

		var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2));
		var endDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId, startDate: startDate, endDate: endDate);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseListSuccess(response);

		// Для startDate не совсем корректно работает часто есть заказы созданные ранее
		// (в документации написано, что startDate и endDate это "Дата операции", а какой именно неизвестно).
		Assert.All(response.Result!, order =>
		{
			Assert.True(order.OrderDate <= endDate.ToDateTime(TimeOnly.MaxValue),
				$"Заказ с идентификатором {order.Id} имеет дату {order.OrderDate}, которая выходит за пределы указанного диапазона ({startDate} - {endDate}).");
		});
	}

	/// <summary>
	/// Проверяет получение заказа с определенным id.
	/// </summary>
	[Fact]
	public async Task GetOrderListWithOrderIdAsync()
	{
		CheckCityIsNotNull();

		// Сначала получение списка заказов и потом взять любой id
		var orders = await GetOrdersAsync();

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId,
			orders.Result![0].Id);
		var response = await _client.GetOrderListAsync(request);
		AssertResponseListSuccess(response);

		Assert.True(response.Result!.Count == 1);
	}

	/// <summary>
	/// Проверяет получение списка заказов cо статусом аннулирован.
	/// </summary>
	[Fact]
	public async Task GetOrderListWithAnnulateStatusAsync()
	{
		CheckCityIsNotNull();

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId, status: OrderStatus.Annulate);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseListSuccess(response);

		Assert.All(response.Result!, order =>
		{
			Assert.True(order.Status == OrderStatus.Annulate,
				$"Заказ с идентификатором {order.Id} статус отличный от аннулирован {order.OrderDate}.");
		});
	}


	/// <summary>
	/// Проверяет получение информации об 1 заказе.
	/// </summary>
	[Fact]
	public async Task GetOrderInfoAsync()
	{
		CheckCityIsNotNull();

		// Сначала получение списка заказов и потом взять любой id
		var orders = await GetOrdersAsync();

		var request = new GetOrderInfoRequest(_auth, _crmTestData.CityId, orders.Result![0].Id);
		var response = await _client.GetOrderInfoAsync(request);

		AssertResponseListSuccess(response);

		Assert.True(response.Result!.Count == 1);
	}

	/// <summary>
	/// Проверяет получение информации о паре заказов.
	/// </summary>
	[Fact]
	public async Task GetOrdersInfoAsync()
	{
		CheckCityIsNotNull();

		// Сначала получение списка заказов и потом взять любой id
		var orders = await GetOrdersAsync();

		int[] ordersId = orders.Result!.Count > 1
			? orders.Result.Take(2).Select(o => o.Id).ToArray()
			: [orders.Result[0].Id];

		var request = new GetOrderInfoRequest(_auth, _crmTestData.CityId, ordersId);
		var response = await _client.GetOrderInfoAsync(request);

		AssertResponseListSuccess(response);

		Assert.True(response.Result!.Count == ordersId.Length);
	}




	#region Вспомогательные методы
	private void CheckCityIsNotNull()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			Assert.Fail("Не указан идентификатор города.");
	}

	// Вспомогательный метод для общих проверок
	private static void AssertResponseListSuccess<T>(ResponseBase<List<T>> response)
	{
		Assert.True(response.Status == ResponseStatus.Success, response.Error);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}

	// Метод для получения списка заказов
	private async Task<OrderListResponse> GetOrdersAsync()
	{
		var request = new GetOrderListRequest(_auth, _crmTestData.CityId, status: OrderStatus.Annulate);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseListSuccess(response);
		return response;
	}
	#endregion
}
