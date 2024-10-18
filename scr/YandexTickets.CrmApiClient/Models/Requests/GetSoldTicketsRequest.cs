using YandexTickets.Common.Models.Requests;
using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Requests;

/// <summary>
/// Запрос для получения списка проданных билетов.
/// </summary>
/// <remarks>Метод не описан в документации API.</remarks>
public class GetSoldTicketsRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор запроса для получения списка проданных билетов.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="eventId">Идентификатор события</param>
	/// <param name="startDate">Дата начиная с которой (включительно) билеты возвращаются в ответе.</param>
	/// <param name="endDate">Дата до которой (включительно) билеты возвращаются в ответе.</param>
	public GetSoldTicketsRequest(string auth, string cityId, int? eventId = null,
		DateOnly? startDate = null, DateOnly? endDate = null)
		: base(auth, cityId) { }

	protected override string Action => "crm.order.ticket.list";

	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[QueryParameter("event_id", false)]
	public int? EventId { get; set; }

	/// <summary>
	/// Дата начиная с которой (включительно) билеты возвращаются в ответе.
	/// </summary>
	[QueryParameter("start_date", false)]
	public DateOnly? StartDate { get; set; }

	/// <summary>
	/// Дата до которой (включительно) билеты возвращаются в ответе.
	/// </summary>
	[QueryParameter("end_date", false)]
	public DateOnly? EndDate { get; set; }
}
