using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Crm.Models.DTO;

namespace YandexTickets.ApiClients.Crm.Models.Responses;

/// <summary>
/// Ответ, содержащий информацию о заказах.
/// </summary>
public class OrderInfoResponse : ResponseBase<List<OrderInfo>> { }
