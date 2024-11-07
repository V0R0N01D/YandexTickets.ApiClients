using YandexTickets.ApiClients.Agent.Models.Requests;
using YandexTickets.ApiClients.Agent.Models.Responses;
using YandexTickets.ApiClients.Common;

namespace YandexTickets.ApiClients.Agent;

/// <summary>
/// Клиент для взаимодействия с API агентского сервиса Яндекс.Билеты.
/// </summary>
public sealed class YandexTicketAgentApiClient : YandexTicketsApiClientBase, IYandexTicketsAgentApiClient
{
    private const string BaseUrl = "https://api.tickets.yandex.net/api/agent/";

    /// <summary>
    /// Конструктор клиента для взаимодействия с API Agent Яндекс.Билетов.
    /// </summary>
    /// <param name="login"><inheritdoc/></param>
    /// <param name="password"><inheritdoc/></param>
    /// <param name="client">HttpClient который будет использоваться для запросов.</param>
    public YandexTicketAgentApiClient(string login, string password, HttpClient client)
        : base(login, password, client)
    {
        client.BaseAddress = new Uri(BaseUrl);
    }

    public Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<CityListResponse>(GetRequestString(request), cancellationToken);
    }
}