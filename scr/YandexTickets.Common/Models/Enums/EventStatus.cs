using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace YandexTickets.Common.Models.Enums;

/// <summary>
/// Статус события или мероприятия.
/// Значения:
/// Close - Закрыто;
/// OnSale - В продаже
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
