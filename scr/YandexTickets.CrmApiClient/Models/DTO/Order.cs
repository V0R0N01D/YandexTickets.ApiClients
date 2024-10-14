using System.Text.Json.Serialization;
using YandexTickets.Common.Models.Enums;
using YandexTickets.Common.Services.Converters;
using YandexTickets.CrmApiClient.Models.Enums;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Заказ.
/// </summary>
public record Order
{
	/// <summary>
	/// Идентификатор заказа.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Идентификатор покупателя.
	/// </summary>
	[JsonPropertyName("customer_id")]
	public int CustomerId { get; set; }

	/// <summary>
	/// Данные покупателя.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API.</remarks>
	[JsonPropertyName("customer")]
	public Customer? Customer { get; set; }

	/// <summary>
	/// Статус заказа.
	/// </summary>
	[JsonPropertyName("status")]
	public OrderStatus Status { get; set; }

	/// <summary>
	/// Признак возврата заказа.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API. <br/>
	/// (Известные значения 0 - заказ не возвращен, 1 - заказ возвращен).</remarks>
	[JsonPropertyName("is_returned")]
	public int? IsReturned { get; set; }

	/// <summary>
	/// Дата создания заказа.
	/// </summary>
	[JsonPropertyName("order_date")]
	[JsonConverter(typeof(YandexDateTimeOffsetConverter))]
	public DateTimeOffset OrderDate { get; set; }

	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[JsonPropertyName("event_id")]
	public int EventId { get; set; }

	/// <summary>
	/// Идентификатор абонемента.
	/// </summary>
	[JsonPropertyName("season_id")]
	public int SeasonId { get; set; }

	/// <summary>
	/// Количество билетов в заказе.
	/// </summary>
	[JsonPropertyName("tickets_count")]
	public int TicketsCount { get; set; }

	/// <summary>
	/// Количество абонементов в заказе.
	/// </summary>
	[JsonPropertyName("seasons_count")]
	public int SeasonsCount { get; set; }

	/// <summary>
	/// Сумма заказа.
	/// </summary>
	[JsonPropertyName("sum")]
	public int Sum { get; set; }

	/// <summary>
	/// Сумма сервисного сбора.
	/// </summary>
	[JsonPropertyName("fee")]
	public int Fee { get; set; }

	/// <summary>
	/// Тип продажи.
	/// </summary>
	[JsonPropertyName("sale_type")]
	public SaleType SaleType { get; set; }

	/// <summary>
	/// Идентификатор агента.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API.</remarks>
	[JsonPropertyName("agent_id")]
	public int? AgentId { get; set; }
}
