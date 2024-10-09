using YandexTicketsCommon.Models.DTO;
using YandexTicketsCommon.Models.Responses;

namespace YandexTicketsCRMClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список городов.
/// </summary>
public class CityListResponse : ResponseBase<City[][]> { }
