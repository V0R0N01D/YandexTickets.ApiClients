using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Common.Models.Enums;

/// <summary>
/// Статус события или мероприятия.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>Close</term>
/// <description>Закрыто</description>
/// </item>
/// <item>
/// <term>OnSale</term>
/// <description>В продаже</description>
/// </item>
/// </list>
/// </remarks>
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
