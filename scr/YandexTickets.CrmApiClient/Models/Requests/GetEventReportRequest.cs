using YandexTickets.Common.Models.Requests;
using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Requests;

/// <summary>
/// Класса запроса для получения отчета по событиям.
/// </summary>
public class GetEventReportRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса отчета по событиям.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="eventsId">Один или несколько идентификаторов событий.</param>
	public GetEventReportRequest(string auth, string cityId, params int[] eventsId)
		: base(auth, cityId)
	{
		if (eventsId == null || eventsId.Length == 0)
			throw new ArgumentNullException(nameof(eventsId),
				"Необходимо указать хотя бы один идентификатор события.");

		EventsId = eventsId;
	}

	protected override string Action => "crm.report.event";

	/// <summary>
	/// Идентификаторы событий.
	/// </summary>
	[QueryParameter("event_ids")]
	public int[] EventsId { get; set; }
}
