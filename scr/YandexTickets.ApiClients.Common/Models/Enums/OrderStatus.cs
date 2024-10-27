namespace YandexTickets.ApiClients.Common.Models.Enums;

/// <summary>
/// Статус заказа.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>Annulate</term>
/// <description>Аннулирован</description>
/// </item>
/// <item>
/// <term>Sale</term>
/// <description>Продан</description>
/// </item>
/// <item>
/// <term>Booking</term>
/// <description>Бронь</description>
/// </item>
/// <item>
/// <term>Delivery</term>
/// <description>Доставка</description>
/// </item>
/// </list>
/// </remarks>
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
