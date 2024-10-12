using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;
using YandexTickets.CrmApiClient.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список событий.
/// </summary>
[SingleElementArray]
public class EventListResponse : ResponseBase<List<Event>> { }
