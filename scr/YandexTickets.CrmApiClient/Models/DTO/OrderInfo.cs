using System.Text.Json.Serialization;
using YandexTickets.Common.Models.Enums;
using YandexTickets.Common.Services.Converters;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Детальная информация о заказе.
/// </summary>
public record OrderInfo
{
	/// <summary>
	/// Идентификатор заказа.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Статус заказа.
	/// </summary>
	[JsonPropertyName("status")]
	public OrderStatus Status { get; set; }

	/// <summary>
	/// Идентификатор агента.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API.</remarks>
	[JsonPropertyName("agent_id")]
	public int? AgentId { get; set; }

	/// <summary>
	/// Признак возврата заказа.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API. <br/>
	/// (Известные значения 0 - заказ не возвращен, 1 - заказ возвращен).</remarks>
	[JsonPropertyName("is_returned")]
	public int? IsReturned { get; set; }

	/// <summary>
	/// Данные покупателя.
	/// </summary>
	[JsonPropertyName("customer")]
	public required Customer Customer { get; set; }

	/// <summary>
	/// Массив билетов в заказе.
	/// </summary>
	[JsonPropertyName("tickets")]
	public required List<Ticket> Tickets { get; set; }

	/// <summary>
	/// Массив абонементов в заказе.
	/// </summary>
	[JsonPropertyName("season_places")]
	public required List<SeasonPlace> SeasonPlaces { get; set; }

	/// <summary>
	/// Дата создания заказа.
	/// </summary>
	[JsonPropertyName("order_date")]
	[JsonConverter(typeof(YandexDateTimeOffsetConverter))]
	public DateTimeOffset OrderDate { get; set; }

	/// <summary>
	/// Дата аннулирования заказа.
	/// </summary>
	[JsonPropertyName("annulate_date")]
	[JsonConverter(typeof(YandexDateTimeOffsetConverter))]
	public DateTimeOffset? AnnulateDate { get; set; }
}
