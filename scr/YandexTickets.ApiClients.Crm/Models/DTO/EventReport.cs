using System.Text.Json.Serialization;
using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Common.Services.Converters.Response;

namespace YandexTickets.ApiClients.Crm.Models.DTO;

public record EventReport
{
	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[JsonPropertyName("event_id")]
	public int Id { get; set; }

	/// <summary>
	/// Название события.
	/// </summary>
	[JsonPropertyName("event_name")]
	public required string EventName { get; set; }

	/// <summary>
	/// Дата события.
	/// </summary>
	[JsonPropertyName("event_date")]
	[JsonConverter(typeof(YandexDateTimeConverter))]
	public DateTime EventDate { get; set; }

	/// <summary>
	/// Идентификатор мероприятия.
	/// </summary>
	[JsonPropertyName("activity_id")]
	public required int ActivityId { get; set; }

	/// <summary>
	/// Название агента.
	/// </summary>
	[JsonPropertyName("agent")]
	public required string Agent { get; set; }

	/// <summary>
	/// Идентификатор агента.
	/// </summary>
	[JsonPropertyName("agent_id")]
	public required string AgentId { get; set; }

	/// <summary>
	/// Зависит от значения параметра Agent:
	/// если "-", то это количество билетов, заведенных в продажу, за вычетом билетов, выданных офлайн-агентам на распространение;
	/// если отличается от "-", то это количество билетов, выданных офлайн-агенту на распространение.
	/// </summary>
	[JsonPropertyName("tickets_count")]
	public int TicketsCount { get; set; }

	/// <summary>
	/// Количество доступных для продажи билетов.
	/// </summary>
	[JsonPropertyName("tickets_available")]
	public int TicketsAvailable { get; set; }

	/// <summary>
	/// Количество забронированных билетов.
	/// </summary>
	[JsonPropertyName("tickets_booked")]
	public int TicketsBooked { get; set; }

	/// <summary>
	/// Зависит от значения параметра Agent:
	/// если "-", то это количество билетов, проданных без участия агентов (продажи в кассе или в личном кабинете);
	/// если отличается от "-", то это количество проданных агентом билетов.
	/// </summary>
	[JsonPropertyName("tickets_sold")]
	public int TicketsSold { get; set; }

	/// <summary>
	/// Зависит от значения параметра Agent:
	/// если "-", то значение будет 0;
	/// если отличается от "-", то это количество выданных офлайн-агенту билетов, которые еще не проданы.
	/// </summary>
	[JsonPropertyName("tickets_agent")]
	public int TicketsAgent { get; set; }

	/// <summary>
	/// Количество билетов, проданных в абонементе.
	/// </summary>
	[JsonPropertyName("tickets_season")]
	public int TicketsSeason { get; set; }

	/// <summary>
	/// Количество возвращенных билетов.
	/// </summary>
	[JsonPropertyName("tickets_returned")]
	public int TicketsReturned { get; set; }

	/// <summary>
	/// Количество билетов, проданных со скидкой.
	/// </summary>
	[JsonPropertyName("tickets_discount")]
	public int TicketsDiscount { get; set; }

	/// <summary>
	/// Общая сумма скидки.
	/// </summary>
	[JsonPropertyName("tickets_discount_sum")]
	public int TicketsDiscountSum { get; set; }

	/// <summary>
	/// Общая сумма продаж.
	/// </summary>
	[JsonPropertyName("tickets_sold_sum")]
	public int TicketsSoldSum { get; set; }
}
