using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using YandexTickets.Common;
using YandexTickets.Common.Models.Responses;
using YandexTickets.Common.Services.Exceptions;
using YandexTickets.CrmApiClient.Models.Requests;
using YandexTickets.CrmApiClient.Models.Responses;
using YandexTickets.CrmApiClient.Services.Attributes;
using YandexTickets.CrmApiClient.Services.Converters;

namespace YandexTickets.CrmApiClient;

/// <summary>
/// Клиент для взаимодействия с API CRM Яндекс.Билетов.
/// </summary>
public class YandexTicketsCrmApiClient : YandexTicketsApiClientBase, IYandexTicketsCrmApiClient
{
	const string BaseUrl = "https://api.tickets.yandex.net/api/crm/";

	/// <summary>
	/// Конструктор клиента для взаимодействия с API CRM Яндекс.Билетов.
	/// </summary>
	/// <param name="client">HttpClient который будет использоваться для запросов.</param>
	public YandexTicketsCrmApiClient(HttpClient client) : base(client)
	{
		client.BaseAddress = new Uri(BaseUrl);
	}

	protected override async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpContent content,
		CancellationToken cancellationToken)
	{
		var options = new JsonSerializerOptions();

		// Проверка есть ли у класса ответ атрибут,
		// при котором будет иная обработка десериализации массива в ответе
		var type = typeof(TResponse);
		var brokenAttribute = type.GetCustomAttribute<SingleElementArrayAttribute>();
		if (brokenAttribute != null)
			options.Converters.Add(new SingleElementArrayConverterFactory());

		var result = await content.ReadFromJsonAsync<TResponse>(options, cancellationToken)
			.ConfigureAwait(false);
		return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
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
