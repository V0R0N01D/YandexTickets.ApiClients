namespace YandexTickets.ApiClients.Crm.Models.Enums;

/// <summary>
/// Согласие покупателя на получение рассылок.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>NotAgreed</term>
/// <description>Не согласен</description>
/// </item>
/// <item>
/// <term>Agreed</term>
/// <description>Согласен</description>
/// </item>
/// </list>
/// </remarks>
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
