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
	readonly IYandexTicketsCrmApiClient _client;
	readonly CrmTestData _crmTestData;
	readonly string _auth;

	const int DefaultLimit = 3;

	public YandexTicketCrmClientIntegrationTests(TestFixture fixture)
	{
		_client = fixture.ServiceProvider.GetRequiredService<IYandexTicketsCrmApiClient>();
		var options = fixture.ServiceProvider.GetRequiredService<IOptions<CrmTestData>>();
		_crmTestData = options.Value
			?? throw new InvalidOperationException("Не создан файл настроек.");

		if (string.IsNullOrWhiteSpace(_crmTestData.Login) || string.IsNullOrWhiteSpace(_crmTestData.Password))
			throw new InvalidOperationException("В настройках не указан логин или пароль.");

		_auth = AuthService.GenerateAuthToken(_crmTestData.Login, _crmTestData.Password);
	}

	[Fact(DisplayName = "Получение списка городов")]
	public async Task GetCityListAsync()
	{
		var request = new GetCityListRequest(_auth);
		var response = await _client.GetCityListAsync(request);

		AssertResponseSuccess(response);
	}


	[Fact(DisplayName = "Получение списка мероприятий")]
	public async Task GetActivityListAsync()
	{
		ValidateCityId();

		var request = new GetActivityListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetActivityListAsync(request);

		AssertResponseSuccess(response);
	}


	[Fact(DisplayName = "Получение списка событий")]
	public async Task GetEventListAsync()
	{
		ValidateCityId();

		var request = new GetEventListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetEventListAsync(request);

		AssertResponseSuccess(response);
	}


	[Fact(DisplayName = "Получение отчета по событиям")]
	public async Task GetEventReportAsync()
	{
		ValidateCityId();

		var request = new GetEventReportRequest(_auth, _crmTestData.CityId, _crmTestData.EventsId);
		var response = await _client.GetEventReportAsync(request);

		AssertResponseSuccess(response);
	}

	#region Тесты заказов

	[Fact(DisplayName = "Получение списка заказов")]
	public async Task GetOrderListAsync()
	{
		ValidateCityId();
		_ = await GetOrdersAsync();
	}

	[Fact(DisplayName = "Получение списка заказов с фильтрацией по датам")]
	public async Task GetOrderListWithDatesParamAsync()
	{
		ValidateCityId();

		var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2));
		var endDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId, startDate: startDate, endDate: endDate);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseSuccess(response);

		// Для startDate не совсем корректно работает
		// т.к даты переданные в запросе это даты последней операции,
		// а в заказе есть только дата создания заказа и нет даты последней операции.
		Assert.All(response.Result!, order =>
		{
			Assert.True(order.OrderDate <= endDate.ToDateTime(TimeOnly.MaxValue),
				$"Заказ с идентификатором {order.Id} имеет дату {order.OrderDate}, которая выходит за пределы указанного диапазона ({startDate} - {endDate}).");
		});
	}

	[Fact(DisplayName = "Получение заказа по идентификатору")]
	public async Task GetOrderListWithOrderIdAsync()
	{
		ValidateCityId();

		var orders = await GetOrdersAsync();
		var orderId = orders.Result![0].Id;

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId, orderId);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count == 1);
		Assert.Equal(orderId, response.Result![0].Id);
	}

	[Fact(DisplayName = "Получение списка аннулированных заказов")]
	public async Task GetOrderListWithAnnulateStatusAsync()
	{
		ValidateCityId();

		var request = new GetOrderListRequest(_auth, _crmTestData.CityId, status: OrderStatus.Annulate);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseSuccess(response);

		Assert.All(response.Result!, order =>
		{
			Assert.True(order.Status == OrderStatus.Annulate,
				$"Заказ с идентификатором {order.Id} статус отличный от аннулирован: {order.Status}.");
		});
	}


	[Fact(DisplayName = "Получение информации о заказе")]
	public async Task GetOrderInfoAsync()
	{
		ValidateCityId();

		var orders = await GetOrdersAsync();
		var orderId = orders.Result![0].Id;

		var request = new GetOrderInfoRequest(_auth, _crmTestData.CityId, orderId);
		var response = await _client.GetOrderInfoAsync(request);

		AssertResponseSuccess(response);
		Assert.Single(response.Result!);
		Assert.Equal(orderId, response.Result![0].Id);
	}

	[Fact(DisplayName = "Получение информации о нескольких заказах")]
	public async Task GetOrdersInfoAsync()
	{
		ValidateCityId();

		var orders = await GetOrdersAsync();
		int[] ordersId = orders.Result!.Take(2).Select(o => o.Id).ToArray();

		var request = new GetOrderInfoRequest(_auth, _crmTestData.CityId, ordersId);
		var response = await _client.GetOrderInfoAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count == ordersId.Length);
	}

	#endregion


	[Fact(DisplayName = "Получение списка покупателей")]
	public async Task GetCustomerListAsync()
	{
		ValidateCityId();

		var request = new GetCustomerListRequest(_auth, _crmTestData.CityId, DefaultLimit);
		var response = await _client.GetCustomerListAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count <= DefaultLimit);
	}



	[Fact(DisplayName = "Получение списка агентов")]
	public async Task GetAgentListAsync()
	{
		ValidateCityId();

		var request = new GetAgentListRequest(_auth, _crmTestData.CityId, DefaultLimit);
		var response = await _client.GetAgentListAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count <= DefaultLimit);
	}



	#region Вспомогательные методы
	private void ValidateCityId()
	{
		if (string.IsNullOrWhiteSpace(_crmTestData.CityId))
			throw new InvalidOperationException("Не указан идентификатор города в настройках тестовых данных.");
	}

	// Вспомогательный метод для общих проверок
	private static void AssertResponseSuccess<T>(ResponseBase<List<T>> response)
	{
		Assert.True(response.Status == ResponseStatus.Success, response.Error);
		Assert.NotNull(response.Result);
		Assert.NotEmpty(response.Result);
	}

	// Метод для получения списка заказов
	private async Task<OrderListResponse> GetOrdersAsync()
	{
		var request = new GetOrderListRequest(_auth, _crmTestData.CityId);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseSuccess(response);
		return response;
	}
	#endregion
}
