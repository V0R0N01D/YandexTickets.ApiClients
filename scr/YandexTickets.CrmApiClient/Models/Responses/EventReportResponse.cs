using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий отчет о событиях.
/// </summary>
public class EventReportResponse : ResponseBase<List<EventReport>> { }
