using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using YandexTickets.Common;
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

		var result = await content.ReadFromJsonAsync<TResponse>(options, cancellationToken);
		return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
	}


	public Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<CityListResponse>(request.GetRequestPath(), cancellationToken);
	}


	public Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<ActivityListResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<EventListResponse> GetEventListAsync(GetEventListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<EventListResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<EventReportResponse> GetEventReportAsync(GetEventReportRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<EventReportResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<OrderListResponse> GetOrderListAsync(GetOrderListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<OrderListResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<OrderInfoResponse> GetOrderInfoAsync(GetOrderInfoRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<OrderInfoResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<CustomerListResponse> GetCustomerListAsync(GetCustomerListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<CustomerListResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<AgentListResponse> GetAgentListAsync(GetAgentListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<AgentListResponse>(request.GetRequestPath(), cancellationToken);
	}

	public Task<UnsubscribeCustomerResponse> UnsubscribeCustomerAsync(UnsubscribeCustomerRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendPostRequestAsync<UnsubscribeCustomerResponse>(request.GetRequestPath(),
			null, cancellationToken);
	}
}
