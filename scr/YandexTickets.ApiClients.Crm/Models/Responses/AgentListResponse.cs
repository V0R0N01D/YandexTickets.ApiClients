using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Crm.Models.DTO;
using YandexTickets.ApiClients.Crm.Services.Attributes;

namespace YandexTickets.ApiClients.Crm.Models.Responses;

/// <summary>
/// Ответ, содержащий список агентов.
/// </summary>
[SingleElementArray]
public class AgentListResponse : ResponseBase<List<Agent>> { }
