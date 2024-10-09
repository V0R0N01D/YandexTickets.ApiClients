using YandexTicketsCommon.Models.Responses;
using YandexTicketsCRMClient.Models.DTO;

namespace YandexTicketsCRMClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список мероприятий.
/// </summary>
public class ActivityListResponse : ResponseBase<Activity[][]> { }
