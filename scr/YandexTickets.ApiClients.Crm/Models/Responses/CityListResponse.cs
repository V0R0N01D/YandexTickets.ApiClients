using YandexTickets.ApiClients.Common.Models.DTO;
using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Crm.Services.Attributes;

namespace YandexTickets.ApiClients.Crm.Models.Responses;

/// <summary>
/// Ответ, содержащий список городов.
/// </summary>
[SingleElementArray]
public class CityListResponse : ResponseBase<List<City>> { }
