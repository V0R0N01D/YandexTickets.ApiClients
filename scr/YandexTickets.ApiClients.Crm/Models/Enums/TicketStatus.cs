using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Crm.Models.Enums;

/// <summary>
/// Определяет статус билета.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>Annulate</term>
/// <description>Аннулирован</description>
/// </item>
/// <item>
/// <term>Active</term>
/// <description>Активен</description>
/// </item>
/// </list>
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketStatus
{
	/// <summary>
	/// Аннулирован
	/// </summary>
	Annulate = 0,

	/// <summary>
	/// Активен
	/// </summary>
	Active = 1
}
