using System.Text.Json.Serialization;
using YandexTickets.Common.Services.Converters.Response;

namespace YandexTickets.Common.Models.DTO;

/// <summary>
/// Информация о проданном билете.
/// </summary>
public record SoldTicket
{
	/// <summary>
	/// Идентификатор билета.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Идентификатор заказа.
	/// </summary>
	[JsonPropertyName("order_id")]
	public int OrderId { get; set; }

	/// <summary>
	/// Штрихкод билета.
	/// </summary>
	[JsonPropertyName("barcode")]
	public required string Barcode { get; set; }

	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[JsonPropertyName("event_id")]
	public int EventId { get; set; }

	/// <summary>
	/// Дата продажи билета.
	/// </summary>
	[JsonPropertyName("sale_date")]
	[JsonConverter(typeof(YandexDateTimeOffsetConverter))]
	public DateTimeOffset SaleDate { get; set; }

	/// <summary>
	/// Стоимость билета.
	/// </summary>
	[JsonPropertyName("price")]
	public decimal Price { get; set; }

	/// <summary>
	/// Идентификатор места.
	/// </summary>
	[JsonPropertyName("place_id")]
	public int PlaceId { get; set; }

	/// <summary>
	/// Идентификатор сектора.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API.</remarks>
	[JsonPropertyName("sector_id")]
	public int? SectorId { get; set; }

	/// <summary>
	/// Название сектора.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API.</remarks>
	[JsonPropertyName("sector_name")]
	public string? SectorName { get; set; }

	/// <summary>
	/// Оригинальная стоимость билета.
	/// </summary>
	/// <remarks>Поле отсутствует в документации API.</remarks>
	[JsonPropertyName("original_price")]
	public decimal? OriginalPrice { get; set; }
}
