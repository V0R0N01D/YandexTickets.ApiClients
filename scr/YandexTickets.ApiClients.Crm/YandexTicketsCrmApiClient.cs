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
    private readonly JsonSerializerOptions _jsonOptionsForSingleAttribute = new();
    private const string BaseUrl = "https://api.tickets.yandex.net/api/crm/";

    /// <summary>
    /// Инициализирует клиент для взаимодействия с API CRM Яндекс.Билетов.
    /// </summary>
    /// <param name="login"><inheritdoc/></param>
    /// <param name="password"><inheritdoc/></param>
    /// <param name="client">Экземпляр клиента для выполнения HTTP-запросов.</param>
    public YandexTicketsCrmApiClient(string login, string password, HttpClient client)
        : base(login, password, client)
    {
        client.BaseAddress = new Uri(BaseUrl);
        _jsonOptionsForSingleAttribute.Converters.Add(new SingleElementArrayConverterFactory());
    }

    /// <inheritdoc />
    protected override JsonSerializerOptions GetSerializerOptions(Type responseType)
    {
        // Проверка есть ли у класса ответ атрибут,
        // при котором будет иная обработка десериализации массива в ответе
        var singleElementAttribute = responseType.GetCustomAttribute<SingleElementArrayAttribute>();
        return singleElementAttribute != null
            ? _jsonOptionsForSingleAttribute
            : base.GetSerializerOptions(responseType);
    }


    /// <inheritdoc/>
    public Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<CityListResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<ActivityListResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<EventListResponse> GetEventListAsync(GetEventListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<EventListResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<EventReportResponse> GetEventReportAsync(GetEventReportRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<EventReportResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<OrderListResponse> GetOrderListAsync(GetOrderListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<OrderListResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<OrderInfoResponse> GetOrderInfoAsync(GetOrderInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<OrderInfoResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<CustomerListResponse> GetCustomerListAsync(GetCustomerListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<CustomerListResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<AgentListResponse> GetAgentListAsync(GetAgentListRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<AgentListResponse>(GetRequestString(request), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<UnsubscribeCustomerResponse> UnsubscribeCustomerAsync(UnsubscribeCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendPostRequestAsync<UnsubscribeCustomerResponse>(GetRequestString(request),
            null, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<SoldTicketsResponse> GetSoldTicketsAsync(GetSoldTicketsRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendGetRequestAsync<SoldTicketsResponse>(GetRequestString(request), cancellationToken);
    }
}