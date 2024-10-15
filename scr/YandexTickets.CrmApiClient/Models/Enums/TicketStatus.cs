namespace YandexTickets.CrmApiClient.Models.Enums;

/// <summary>
/// Статус билета. <br/><br/>
/// Возможные значения: <br/>
/// Annulate - Аннулирован; <br/>
/// Active - Активен; <br/>
/// </summary>
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
