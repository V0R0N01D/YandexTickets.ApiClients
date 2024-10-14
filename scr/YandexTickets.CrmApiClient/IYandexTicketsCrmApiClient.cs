using YandexTickets.CrmApiClient.Models.Requests;
using YandexTickets.CrmApiClient.Models.Responses;

namespace YandexTickets.CrmApiClient;

public interface IYandexTicketsCrmApiClient
{
	Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken ct = default);
	Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request,
		CancellationToken ct = default);
	Task<EventListResponse> GetEventListAsync(GetEventListRequest request,
		CancellationToken ct = default);
	Task<EventReportResponse> GetEventReportAsync(GetEventReportRequest request,
		CancellationToken ct = default);
	Task<OrderListResponse> GetOrderListAsync(GetOrderListRequest request,
		CancellationToken ct = default);
}
