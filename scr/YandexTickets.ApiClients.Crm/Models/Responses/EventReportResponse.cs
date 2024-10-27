using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Crm.Models.DTO;

namespace YandexTickets.ApiClients.Crm.Models.Responses;

/// <summary>
/// Ответ, содержащий отчет о событиях.
/// </summary>
public class EventReportResponse : ResponseBase<List<EventReport>> { }
