using YandexTickets.Common.Models.DTO;

namespace YandexTickets.Common.Models.Responses;

/// <summary>
/// Ответ, содержащий список городов.
/// </summary>
public class CityListResponse : ResponseBase<City[][]> { }
