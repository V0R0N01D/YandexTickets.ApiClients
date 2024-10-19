using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.Requests;
using YandexTickets.CrmApiClient.Models.Responses;

namespace YandexTickets.CrmApiClient;

public interface IYandexTicketsCrmApiClient
{
	/// <summary>
	/// Возвращает список городов.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа, вернёт CityListResponse,
	/// содержащий статус ответа и список городов или ошибку.
	/// </returns>
	Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает список мероприятий.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа, вернёт ActivityListResponse,
	/// содержащий статус ответа и список мероприятий или ошибку.
	/// </returns>
	Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает список всех событий. 
	/// Чтобы получить список всех событий (сеансов) внутри конкретного мероприятия, 
	/// нужно передать параметр ActivityId в GetEventListRequest. 
	/// Чтобы получить информацию по конкретному событию, 
	/// нужно передать параметр EventId в GetEventListRequest.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа, вернёт EventListResponse,
	/// содержащий статус ответа и список событий или ошибку.
	/// </returns>
	Task<EventListResponse> GetEventListAsync(GetEventListRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает отчет о событиях.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа, вернёт EventReportResponse,
	/// содержащий статус ответа и отчет о событиях или ошибку.
	/// </returns>
	Task<EventReportResponse> GetEventReportAsync(GetEventReportRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает список заказов.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа, вернёт OrderListResponse,
	/// содержащий статус ответа и список заказов или ошибку.
	/// </returns>
	Task<OrderListResponse> GetOrderListAsync(GetOrderListRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает детали заказов.
	/// </summary>
	/// <param name="request">Объект, содержащий данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа вернёт OrderInfoResponse,
	/// содержащий статус ответа и детали заказов или ошибку.
	/// </returns>
	Task<OrderInfoResponse> GetOrderInfoAsync(GetOrderInfoRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает список покупателей.
	/// </summary>
	/// <param name="request">Объект, содержащий данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа вернёт CustomerListResponse,
	/// содержащий статус ответа и список покупателей или ошибку.
	/// </returns>
	Task<CustomerListResponse> GetCustomerListAsync(GetCustomerListRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает список агентов.
	/// </summary>
	/// <param name="request">Объект, содержащий данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа вернёт AgentListResponse,
	/// содержащий статус ответа и список агентов или ошибку.
	/// </returns>
	Task<AgentListResponse> GetAgentListAsync(GetAgentListRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Отписывает покупателя от рассылок в системе Яндекс Билеты. 
	/// </summary>
	/// <param name="request">Объект, содержащий данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Ответ, содержащий статус выполнения операции.</returns>
	Task<UnsubscribeCustomerResponse> UnsubscribeCustomerAsync(UnsubscribeCustomerRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает список проданных билетов.
	/// </summary>
	/// <remarks>Метод не описан в документации API.</remarks>
	/// <param name="request">Объект, содержащий данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа вернёт SoldTicketsResponse,
	/// содержащий статус ответа и список проданных билетов или ошибку.
	/// </returns>
	Task<SoldTicketsResponse> GetSoldTicketsAsync(GetSoldTicketsRequest request,
		CancellationToken cancellationToken = default);
}
