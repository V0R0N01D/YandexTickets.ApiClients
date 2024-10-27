using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

/// <summary>
/// Класс запроса для получения списка событий.
/// </summary>
public class GetEventListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка событий.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="activityId">Идентификатор мероприятия.</param>
	/// <param name="eventId">Идентификатор события.</param>
	public GetEventListRequest(string auth, int cityId, int? activityId = null, int? eventId = null)
		: base(auth, cityId)
	{
		ActivityId = activityId;
		EventId = eventId;
	}

	protected override string Action => "crm.event.list";

	/// <summary>
	/// Идентификатор мероприятия.
	/// </summary>
	[QueryParameter("activity_id", false)]
	public int? ActivityId { get; set; }

	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[QueryParameter("event_id", false)]
	public int? EventId { get; set; }
}
