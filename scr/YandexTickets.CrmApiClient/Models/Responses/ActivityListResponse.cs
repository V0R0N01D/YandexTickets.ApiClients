using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список мероприятий.
/// </summary>
public class ActivityListResponse : ResponseBase<Activity[][]> { }
