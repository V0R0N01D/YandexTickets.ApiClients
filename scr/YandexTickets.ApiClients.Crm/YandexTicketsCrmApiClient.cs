using System.Reflection;
using System.Text.Json;
using YandexTickets.ApiClients.Common;
using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Crm.Models.Requests;
using YandexTickets.ApiClients.Crm.Models.Responses;
using YandexTickets.ApiClients.Crm.Services.Attributes;
using YandexTickets.ApiClients.Crm.Services.Converters;

namespace YandexTickets.ApiClients.Crm;

/// <summary>
/// Клиент для взаимодействия с API CRM Яндекс.Билетов.
/// </summary>
public sealed class YandexTicketsCrmApiClient : YandexTicketsApiClientBase, IYandexTicketsCrmApiClient
{
	JsonSerializerOptions jsonOptionsForSingleAttribute = new JsonSerializerOptions();
	const string BaseUrl = "https://api.tickets.yandex.net/api/crm/";

	/// <summary>
	/// Инициализирует клиент для взаимодействия с API CRM Яндекс.Билетов.
	/// </summary>
	/// <param name="client">Экземпляр клиента для выполнения HTTP-запросов.</param>
	public YandexTicketsCrmApiClient(HttpClient client) : base(client)
	{
		client.BaseAddress = new Uri(BaseUrl);
		jsonOptionsForSingleAttribute.Converters.Add(new SingleElementArrayConverterFactory());
	}

	protected override JsonSerializerOptions GetSerializerOptions(Type responseType)
	{
		// Проверка есть ли у класса ответ атрибут,
		// при котором будет иная обработка десериализации массива в ответе
		var singleElementAttribute = responseType.GetCustomAttribute<SingleElementArrayAttribute>();
		if (singleElementAttribute != null)
			return jsonOptionsForSingleAttribute;

		return base.GetSerializerOptions(responseType);
	}


	public Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<CityListResponse>(GetRequestString(request), cancellationToken);
	}


	public Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<ActivityListResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<EventListResponse> GetEventListAsync(GetEventListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<EventListResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<EventReportResponse> GetEventReportAsync(GetEventReportRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<EventReportResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<OrderListResponse> GetOrderListAsync(GetOrderListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<OrderListResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<OrderInfoResponse> GetOrderInfoAsync(GetOrderInfoRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<OrderInfoResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<CustomerListResponse> GetCustomerListAsync(GetCustomerListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<CustomerListResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<AgentListResponse> GetAgentListAsync(GetAgentListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<AgentListResponse>(GetRequestString(request), cancellationToken);
	}

	public Task<UnsubscribeCustomerResponse> UnsubscribeCustomerAsync(UnsubscribeCustomerRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendPostRequestAsync<UnsubscribeCustomerResponse>(GetRequestString(request),
			null, cancellationToken);
	}

	public Task<SoldTicketsResponse> GetSoldTicketsAsync(GetSoldTicketsRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<SoldTicketsResponse>(GetRequestString(request), cancellationToken);
	}
}
