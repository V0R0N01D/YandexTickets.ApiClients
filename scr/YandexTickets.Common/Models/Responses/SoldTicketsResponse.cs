using YandexTickets.Common.Models.DTO;

namespace YandexTickets.Common.Models.Responses;

/// <summary>
/// Ответ, содержащий список проданных билетов.
/// </summary>
public class SoldTicketsResponse : ResponseBase<List<SoldTicket>> { }
