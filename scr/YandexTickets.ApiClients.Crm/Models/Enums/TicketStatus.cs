using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Crm.Models.Enums;

/// <summary>
/// Статус билета. <br/><br/>
/// Возможные значения: <br/>
/// Annulate - Аннулирован; <br/>
/// Active - Активен; <br/>
/// </summary>
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
