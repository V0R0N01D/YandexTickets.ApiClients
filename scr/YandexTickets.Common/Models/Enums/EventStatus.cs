using System.Text.Json.Serialization;

namespace YandexTickets.Common.Models.Enums;

/// <summary>
/// Статус события или мероприятия. <br/><br/>
/// Значения: <br/>
/// Close - Закрыто; <br/>
/// OnSale - В продаже; <br/>
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventStatus
{
	/// <summary>
	/// Закрыто
	/// </summary>
	Close,

	/// <summary>
	/// В продаже
	/// </summary>
	OnSale,
}
