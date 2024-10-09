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
	[EnumMember(Value = "0")]
	Close,

	/// <summary>
	/// В продаже
	/// </summary>
	[EnumMember(Value = "1")]
	OnSale,
}
