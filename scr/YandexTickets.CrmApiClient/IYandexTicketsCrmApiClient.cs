using YandexTickets.CrmApiClient.Models.Requests;
using YandexTickets.CrmApiClient.Models.Responses;

namespace YandexTickets.CrmApiClient;

public interface IYandexTicketsCrmApiClient
{
	Task<CityListResponse> GetCityListAsync(GetCityListRequest request);
	Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request);
	Task<EventListResponse> GetEventListAsync(GetEventListRequest request);
}
