using YandexTickets.ApiClients.Common.Models.DTO;

namespace YandexTickets.ApiClients.Common.Models.Responses;

/// <summary>
/// Ответ, содержащий список проданных билетов.
/// </summary>
public class SoldTicketsResponse : ResponseBase<List<SoldTicket>> { }
