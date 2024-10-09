using YandexTicketsCommon.Models.Responses;
using YandexTicketsCRMClient.Models.DTO;

namespace YandexTicketsCRMClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список событий.
/// </summary>
public class EventListResponse : ResponseBase<Event[][]> { }
