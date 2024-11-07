using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;
using YandexTickets.ApiClients.Common.Services.Converters.Request;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

/// <summary>
/// Класс запроса для получения списка событий.
/// </summary>
public class GetEventListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка событий.
	/// </summary>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="activityId">Идентификатор мероприятия.</param>
	/// <param name="eventId">Идентификатор события.</param>
	/// <param name="startDate">Дата начиная с которой (включительно) события возвращаются в ответе.
	/// (Параметр отсутствует в документации API)
	/// </param>
	/// <param name="endDate">Дата до которой (включительно) события возвращаются в ответе.
	/// (Параметр отсутствует в документации API)
	/// </param>

	public GetEventListRequest(int cityId, int? activityId = null, int? eventId = null,
		DateOnly? startDate = null, DateOnly? endDate = null)
		: base(cityId)
	{
		ActivityId = activityId;
		EventId = eventId;
		StartDate = startDate;
		EndDate = endDate;
	}

	/// <inheritdoc />
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

	/// <summary>
	/// Дата начиная с которой (включительно) события возвращаются в ответе.
	/// </summary>
	/// <remarks>Параметр отсутствует в документации API.</remarks>
	[QueryParameter("start_date", false)]
	[QueryParameterConverter(typeof(DateOnlyConverter))]
	public DateOnly? StartDate { get; set; }

	/// <summary>
	/// Дата до которой (включительно) события возвращаются в ответе.
	/// </summary>
	/// <remarks>Параметр отсутствует в документации API.</remarks>
	[QueryParameter("end_date", false)]
	[QueryParameterConverter(typeof(DateOnlyConverter))]
	public DateOnly? EndDate { get; set; }
}
