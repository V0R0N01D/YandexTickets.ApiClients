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

	/// <summary>
	/// Десериализует ответ полученный в запросе.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="content">Содержимое ответа.</param>
	/// <returns>Десериализованный ответ.</returns>
	protected override async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpContent content,
		CancellationToken ct)
	{
		var options = new JsonSerializerOptions();

		// Проверка есть ли у класса ответ атрибут,
		// при котором будет иная обработка десериализации массива в ответе
		var type = typeof(TResponse);
		var brokenAttribute = type.GetCustomAttribute<SingleElementArrayAttribute>();
		if (brokenAttribute != null)
			options.Converters.Add(new SingleElementArrayConverterFactory());

		var result = await content.ReadFromJsonAsync<TResponse>(options, ct);
		return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
	}

	/// <summary>
	/// Возвращает список городов.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт CityListResponse,
	/// содержащий статус ответа и список городов или ошибку.
	/// </returns>
	public Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken ct = default)
	{
		return SendGetRequestAsync<CityListResponse>(request.GetRequestPath(), ct);
	}

	/// <summary>
	/// Возвращает список мероприятий.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт ActivityListResponse,
	/// содержащий статус ответа и список мероприятий или ошибку.
	/// </returns>
	public Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request,
		CancellationToken ct = default)
	{
		return SendGetRequestAsync<ActivityListResponse>(request.GetRequestPath(), ct);
	}

	/// <summary>
	/// Возвращает список всех событий. 
	/// Чтобы получить список всех событий (сеансов) внутри конкретного мероприятия, 
	/// нужно передать параметр ActivityId в GetEventListRequest. 
	/// Чтобы получить информацию по конкретному событию, 
	/// нужно передать параметр EventId в GetEventListRequest.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт EventListResponse,
	/// содержащий статус ответа и список событий или ошибку.
	/// </returns>
	public Task<EventListResponse> GetEventListAsync(GetEventListRequest request,
		CancellationToken ct = default)
	{
		return SendGetRequestAsync<EventListResponse>(request.GetRequestPath(), ct);
	}

	/// <summary>
	/// Возвращает отчет о событиях.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт EventReportResponse,
	/// содержащий статус ответа и отчет о событиях или ошибку.
	/// </returns>
	public Task<EventReportResponse> GetEventReportAsync(GetEventReportRequest request,
		CancellationToken ct = default)
	{
		return SendGetRequestAsync<EventReportResponse>(request.GetRequestPath(), ct);
	}

	/// <summary>
	/// Возвращает список заказов.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт OrderListResponse,
	/// содержащий статус ответа и список заказов или ошибку.
	/// </returns>
	public Task<OrderListResponse> GetOrderListAsync(GetOrderListRequest request,
		CancellationToken ct = default)
	{
		return SendGetRequestAsync<OrderListResponse>(request.GetRequestPath(), ct);
	}

	/// <summary>
	/// Возвращает детали заказов.
	/// </summary>
	/// <param name="request">Объект, содержащий данные для запроса.</param>
	/// <returns>
	/// При наличии ответа вернёт OrderInfoResponse,
	/// содержащий статус ответа и детали заказов или ошибку.
	/// </returns>
	public Task<OrderInfoResponse> GetOrderInfoAsync(GetOrderInfoRequest request,
		CancellationToken ct = default)
	{
		return SendGetRequestAsync<OrderInfoResponse>(request.GetRequestPath(), ct);
	}
}
