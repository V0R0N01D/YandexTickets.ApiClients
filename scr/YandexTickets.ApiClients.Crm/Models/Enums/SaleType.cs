namespace YandexTickets.ApiClients.Crm.Models.Enums;

/// <summary>
/// Определяет тип продажи заказа.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>Annulate</term>
/// <description>Аннулирован</description>
/// </item>
/// <item>
/// <term>Invitation</term>
/// <description>Пригласительные по 0 рублей</description>
/// </item>
/// <item>
/// <term>CashRegister</term>
/// <description>Продажа в кассе</description>
/// </item>
/// <item>
/// <term>PersonalAccount</term>
/// <description>Продажа в личном кабинете</description>
/// </item>
/// <item>
/// <term>Gateway</term>
/// <description>Продажа по шлюзу</description>
/// </item>
/// </list>
/// </remarks>
public enum SaleType
{
	/// <summary>
	/// Аннулирован
	/// </summary>
	Annulate,

	/// <summary>
	/// Пригласительные по 0 рублей
	/// </summary>
	Invitation,

	/// <summary>
	/// В кассе
	/// </summary>
	CashRegister,

	/// <summary>
	/// В личном кабинете
	/// </summary>
	PersonalAccount,

	/// <summary>
	/// По шлюзу
	/// </summary>
	Gateway
}
