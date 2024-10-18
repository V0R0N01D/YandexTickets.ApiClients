using YandexTickets.Common.Models.Requests;
using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Requests;

/// <summary>
/// Запрос для отписки покупателя от рассылок в системе Яндекс Билеты.
/// </summary>
public class UnsubscribeCustomerRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор запроса для отписки покупателя от рассылок.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="email">Электронная почта покупателя для отписки.</param>
	public UnsubscribeCustomerRequest(string auth, string cityId, string email)
		: base(auth, cityId)
	{
		Email = email;
	}

	protected override string Action => "crm.customer.unsubscribe";

	/// <summary>
	/// Электронная почта покупателя для отписки.
	/// </summary>
	[QueryParameter("email")]
	public string Email { get; set; }
}
