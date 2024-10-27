using YandexTickets.ApiClients.Common.Models.Requests;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

/// <summary>
/// Класс запроса для получения списка мероприятий.
/// </summary>
public class GetActivityListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка мероприятий.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	public GetActivityListRequest(string auth, int cityId) : base(auth, cityId) { }

	protected override string Action => "crm.activity.list";
}
