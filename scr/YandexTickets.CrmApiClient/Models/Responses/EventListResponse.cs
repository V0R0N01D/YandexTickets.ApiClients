using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список событий.
/// </summary>
public class EventListResponse : ResponseBase<Event[][]> { }
