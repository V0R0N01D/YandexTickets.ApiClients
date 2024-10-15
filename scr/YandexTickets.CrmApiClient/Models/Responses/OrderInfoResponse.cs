using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий информацию о заказах.
/// </summary>
public class OrderInfoResponse : ResponseBase<List<OrderInfo>> { }
