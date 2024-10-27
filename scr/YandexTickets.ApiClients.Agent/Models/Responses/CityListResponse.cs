using YandexTickets.ApiClients.Common.Models.DTO;
using YandexTickets.ApiClients.Common.Models.Responses;

namespace YandexTickets.ApiClients.Agent.Models.Responses;

/// <summary>
/// Ответ, содержащий список городов.
/// </summary>
public class CityListResponse : ResponseBase<List<City>> { }
