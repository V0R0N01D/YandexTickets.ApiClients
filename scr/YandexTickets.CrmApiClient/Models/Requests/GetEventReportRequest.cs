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
	/// <param name="eventIds">Один или несколько идентификаторов событий.</param>
	public GetEventReportRequest(string auth, string cityId, params int[] eventIds)
		: base(auth, cityId)
	{
		if (eventIds == null || eventIds.Length == 0)
			throw new ArgumentNullException(nameof(eventIds),
				"Необходимо указать хотя бы один идентификатор события.");

		EventIds = eventIds;
	}

	protected override string Action => "crm.report.event";

	/// <summary>
	/// Идентификаторы событий.
	/// </summary>
	[QueryParameter("event_ids", false)]
	public int[] EventIds { get; set; }
}
