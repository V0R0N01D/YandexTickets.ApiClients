using System.Text.Json.Serialization;
using YandexTickets.ApiClients.Crm.Models.Enums;

namespace YandexTickets.ApiClients.Crm.Models.DTO;

/// <summary>
/// Абонемент в заказе.
/// </summary>
public record SeasonPlace
{
	/// <summary>
	/// Идентификатор билета абонемента.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Идентификатор абонемента.
	/// </summary>
	[JsonPropertyName("season_id")]
	public int SeasonId { get; set; }

	/// <summary>
	/// Статус билета абонемента.
	/// </summary>
	[JsonPropertyName("status")]
	public TicketStatus Status { get; set; }

	/// <summary>
	/// Цена билета абонемента.
	/// </summary>
	[JsonPropertyName("price")]
	public int Price { get; set; }

	/// <summary>
	/// Цена билета абонемента без скидки.
	/// </summary>
	[JsonPropertyName("original_price")]
	public int OriginalPrice { get; set; }

	/// <summary>
	/// Сервисный сбор.
	/// </summary>
	[JsonPropertyName("fee")]
	public int Fee { get; set; }

	/// <summary>
	/// Сервисный сбор без скидки.
	/// </summary>
	[JsonPropertyName("original_fee")]
	public int OriginalFee { get; set; }

	/// <summary>
	/// Тип места.
	/// </summary>
	[JsonPropertyName("type")]
	public PlaceType Type { get; set; }

	/// <summary>
	/// Название сектора.
	/// </summary>
	[JsonPropertyName("sector")]
	public required string Sector { get; set; }

	/// <summary>
	/// Идентификатор сектора.
	/// </summary>
	[JsonPropertyName("sector_id")]
	public int SectorId { get; set; }

	/// <summary>
	/// Идентификатор места.
	/// </summary>
	[JsonPropertyName("place_id")]
	public int PlaceId { get; set; }

	/// <summary>
	/// Номер ряда.
	/// </summary>
	[JsonPropertyName("row")]
	public required string Row { get; set; }

	/// <summary>
	/// Номер места.
	/// </summary>
	[JsonPropertyName("place")]
	public required string Place { get; set; }

	/// <summary>
	/// Версия зала.
	/// </summary>
	[JsonPropertyName("venue_version")]
	public required string VenueVersion { get; set; }

	/// <summary>
	/// Название площадки.
	/// </summary>
	[JsonPropertyName("venue")]
	public required string Venue { get; set; }

	/// <summary>
	/// RFID абонемента.
	/// </summary>
	[JsonPropertyName("rfid")]
	public required string Rfid { get; set; }

	/// <summary>
	/// Штрихкод.
	/// </summary>
	[JsonPropertyName("barcode")]
	public required string Barcode { get; set; }

	/// <summary>
	/// Мастер-место (может быть null).
	/// </summary>
	[JsonPropertyName("master_seat")]
	public object? MasterSeat { get; set; }
}
