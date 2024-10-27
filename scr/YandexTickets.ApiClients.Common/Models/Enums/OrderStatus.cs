namespace YandexTickets.ApiClients.Common.Models.Enums;

/// <summary>
/// Статус заказа. <br/><br/>
/// Значения: <br/>
/// Annulate - Аннулирован; <br/>
/// Sale - Продан; <br/>
/// Booking - Бронь; <br/>
/// Delivery - Доставка; <br/>
/// </summary>
public enum OrderStatus
{
	/// <summary>
	/// Аннулирован
	/// </summary>
	Annulate,

	/// <summary>
	/// Продан
	/// </summary>
	Sale,

	/// <summary>
	/// Бронь
	/// </summary>
	Booking,

	/// <summary>
	/// Доставка
	/// </summary>
	Delivery,
}
