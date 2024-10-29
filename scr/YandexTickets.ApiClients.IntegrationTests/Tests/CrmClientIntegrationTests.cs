using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Crm;
using YandexTickets.ApiClients.Crm.Models.Requests;
using YandexTickets.ApiClients.Crm.Models.Responses;
using YandexTickets.ApiClients.IntegrationTests.Models;
using YandexTickets.ApiClients.IntegrationTests.Common;
using YandexTickets.ApiClients.IntegrationTests.Fixtures;

namespace YandexTickets.ApiClients.IntegrationTests.Tests;

/// <summary>
/// Интеграционные тесты для методов YandexTicketsCrmApiClient.
/// </summary>
public class CrmClientIntegrationTests
	: BaseIntegrationTests<IYandexTicketsCrmApiClient, CrmTestData, CrmFixture>
{
	const int DefaultLimit = 3;

	public CrmClientIntegrationTests(CrmFixture fixture) : base(fixture) { }

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
		var request = new GetActivityListRequest(_auth, GetCityId());
		var response = await _client.GetActivityListAsync(request);

		AssertResponseSuccess(response);
	}

	#region Тесты получения списка событий
	[Fact(DisplayName = "Получение списка событий")]
	public async Task GetEventListAsync()
	{
		var request = new GetEventListRequest(_auth, GetCityId());
		var response = await _client.GetEventListAsync(request);

		AssertResponseSuccess(response);
	}

	[Fact(DisplayName = "Получение списка событий с фильтрацией по датам")]
	public async Task GetEventListWithDatesParamAsync()
	{
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2));
		var endDate = DateOnly.FromDateTime(DateTime.Now);

		var request = new GetEventListRequest(_auth, GetCityId(), startDate: startDate, endDate: endDate);
		var response = await _client.GetEventListAsync(request);

		AssertResponseSuccess(response);

		Assert.All(response.Result!, currentEvent =>
		{
			Assert.True(currentEvent.Date >= startDate.ToDateTime(TimeOnly.MinValue)
				&& currentEvent.Date <= endDate.ToDateTime(TimeOnly.MaxValue),
				$"Событие с идентификатором {currentEvent.Id} имеет дату {currentEvent.Date}, которая выходит за пределы указанного диапазона ({startDate} - {endDate}).");
		});
	}
	#endregion


	[Fact(DisplayName = "Получение отчета по событиям")]
	public async Task GetEventReportAsync()
	{
		var request = new GetEventReportRequest(_auth, GetCityId(), _testData.EventsId);
		var response = await _client.GetEventReportAsync(request);

		AssertResponseSuccess(response);
	}

	#region Тесты заказов

	[Fact(DisplayName = "Получение списка заказов")]
	public async Task GetOrderListAsync()
	{
		_ = await GetOrdersAsync();
	}

	[Fact(DisplayName = "Получение списка заказов с фильтрацией по датам")]
	public async Task GetOrderListWithDatesParamAsync()
	{
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2));
		var endDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));

		var request = new GetOrderListRequest(_auth, GetCityId(), startDate: startDate, endDate: endDate);
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
		var orders = await GetOrdersAsync();
		var orderId = orders.Result![0].Id;

		var request = new GetOrderListRequest(_auth, GetCityId(), orderId);
		var response = await _client.GetOrderListAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count == 1);
		Assert.Equal(orderId, response.Result![0].Id);
	}

	[Fact(DisplayName = "Получение списка аннулированных заказов")]
	public async Task GetOrderListWithAnnulateStatusAsync()
	{
		var request = new GetOrderListRequest(_auth, GetCityId(), status: OrderStatus.Annulate);
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
		var orders = await GetOrdersAsync();
		var orderId = orders.Result![0].Id;

		var request = new GetOrderInfoRequest(_auth, GetCityId(), orderId);
		var response = await _client.GetOrderInfoAsync(request);

		AssertResponseSuccess(response);
		Assert.Single(response.Result!);
		Assert.Equal(orderId, response.Result![0].Id);
	}

	[Fact(DisplayName = "Получение информации о нескольких заказах")]
	public async Task GetOrdersInfoAsync()
	{
		var orders = await GetOrdersAsync();
		int[] ordersId = orders.Result!.Take(2).Select(o => o.Id).ToArray();

		var request = new GetOrderInfoRequest(_auth, GetCityId(), ordersId);
		var response = await _client.GetOrderInfoAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count == ordersId.Length);
	}

	#endregion


	[Fact(DisplayName = "Получение списка покупателей")]
	public async Task GetCustomerListAsync()
	{
		var request = new GetCustomerListRequest(_auth, GetCityId(), DefaultLimit);
		var response = await _client.GetCustomerListAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count <= DefaultLimit);
	}



	[Fact(DisplayName = "Получение списка агентов")]
	public async Task GetAgentListAsync()
	{
		var request = new GetAgentListRequest(_auth, GetCityId(), DefaultLimit);
		var response = await _client.GetAgentListAsync(request);

		AssertResponseSuccess(response);
		Assert.True(response.Result!.Count <= DefaultLimit);
	}



	[Fact(DisplayName = "Отписывает покупателей от рассылок в системе Яндекс Билеты.")]
	public async Task UnsubscribeCustomerAsync()
	{
		var request = new UnsubscribeCustomerRequest(_auth, GetCityId(), _testData.Email);
		var response = await _client.UnsubscribeCustomerAsync(request);

		Assert.True(response.Status == ResponseStatus.Success, response.Error);
		Assert.True(response.Result);
	}


	[Fact(DisplayName = "Получение списка проданных билетов")]
	public async Task GetSoldTicketListAsync()
	{
		var request = new GetSoldTicketsRequest(_auth, GetCityId());
		var response = await _client.GetSoldTicketsAsync(request);

		Assert.True(response.Status == ResponseStatus.Success, response.Error);
		AssertResponseSuccess(response);
	}

	#region Вспомогательные методы
	// Метод для получения списка заказов
	private async Task<OrderListResponse> GetOrdersAsync()
	{
		var request = new GetOrderListRequest(_auth, GetCityId());
		var response = await _client.GetOrderListAsync(request);

		AssertResponseSuccess(response);
		return response;
	}
	#endregion
}
