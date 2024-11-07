using YandexTickets.ApiClients.Common.Models.Requests;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

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

    /// <inheritdoc />
    protected override string Action => "crm.city.list";
}