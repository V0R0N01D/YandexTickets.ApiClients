namespace YandexTickets.CrmApiClient.Models.Enums;

/// <summary>
/// Тип продажи заказа. <br/><br/>
/// Значения: <br/>
/// Annulate - Аннулирован; <br/>
/// Invitation - Пригласительные по 0 рублей; <br/>
/// CashRegister - В кассе; <br/>
/// PersonalAccount - В личном кабинете; <br/>
/// Gateway - По шлюзу; <br/>
/// </summary>
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
