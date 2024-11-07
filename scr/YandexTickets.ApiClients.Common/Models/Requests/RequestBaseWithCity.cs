using YandexTickets.ApiClients.Common.Services.Attributes;

namespace YandexTickets.ApiClients.Common.Models.Requests;

/// <summary>
/// Базовый класс запроса дополнительно содержащий идентификатор города.
/// </summary>
public abstract class RequestBaseWithCity : RequestBase
{
    /// <summary>
    /// Конструктор для создания запроса с идентификатором города.
    /// </summary>
    /// <param name="cityId">Идентификатор города.</param>
    protected RequestBaseWithCity(int cityId)
    {
        CityId = cityId;
    }

    /// <summary>
    /// Идентификатор города.
    /// </summary>
    [QueryParameter("city_id")]
    public int CityId { get; set; }
}