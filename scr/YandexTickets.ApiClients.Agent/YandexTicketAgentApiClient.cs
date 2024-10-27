using YandexTickets.ApiClients.Agent;
using YandexTickets.ApiClients.Agent.Models.Requests;
using YandexTickets.ApiClients.Agent.Models.Responses;
using YandexTickets.ApiClients.Common;

namespace YandexTicketsAgentClient;

/// <summary>
/// Клиент для взаимодействия с API агентского сервиса Яндекс.Билеты.
/// </summary>
public sealed class YandexTicketAgentApiClient : YandexTicketsApiClientBase, IYandexTicketsAgentApiClient
{
	const string BaseUrl = "https://api.tickets.yandex.net/api/agent/";

	/// <summary>
	/// Конструктор клиента для взаимодействия с API Agent Яндекс.Билетов.
	/// </summary>
	/// <param name="client">HttpClient который будет использоваться для запросов.</param>
	public YandexTicketAgentApiClient(HttpClient client) : base(client)
	{
		client.BaseAddress = new Uri(BaseUrl);
	}

	public Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken cancellationToken = default)
	{
		return SendGetRequestAsync<CityListResponse>(GetRequestString(request), cancellationToken);
	}
}
