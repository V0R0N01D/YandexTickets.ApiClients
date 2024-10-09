using YandexTickets.Common.Models.Requests;

namespace YandexTickets.CrmApiClient.Models.Requests;

/// <summary>
/// Класс запроса получения списка мероприятий.
/// </summary>
public class GetActivityListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка мероприятий.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	public GetActivityListRequest(string auth, string cityId) : base(auth, cityId) { }

	protected override string Action => "crm.activity.list";
}
