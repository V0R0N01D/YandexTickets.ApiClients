namespace YandexTickets.CrmApiClient.Models.Enums;

/// <summary>
/// Согласен ли покупатель на получение рассылок. <br/><br/>
/// Возможные значения: <br/>
/// NotAgreed - Не согласен; <br/>
/// Agreed - Согласен; <br/>
/// </summary>
public enum CustomerConsent
{
	/// <summary>
	/// Не согласен
	/// </summary>
	NotAgreed = 0,

	/// <summary>
	/// Согласен
	/// </summary>
	Agreed = 1
}
