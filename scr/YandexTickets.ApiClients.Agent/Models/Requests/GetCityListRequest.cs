using YandexTickets.ApiClients.Common.Models.Requests;

namespace YandexTickets.ApiClients.Agent.Models.Requests;

/// <summary>
/// Запрос для получения списка городов.
/// </summary>
public class GetCityListRequest : RequestBase
{
    /// <summary>
    /// Конструктор запроса для получения списка городов.
    /// </summary>
    public GetCityListRequest()
    {
    }

    protected override string Action => "city.list";
}