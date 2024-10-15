using System.Text.Json.Serialization;
using YandexTickets.CrmApiClient.Models.Enums;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Билет в заказе.
/// </summary>
public record Ticket
{
	/// <summary>
	/// Идентификатор билета.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[JsonPropertyName("event_id")]
	public int EventId { get; set; }

	/// <summary>
	/// Статус билета.
	/// </summary>
	[JsonPropertyName("status")]
	public TicketStatus Status { get; set; }

	/// <summary>
	/// Цена билета.
	/// </summary>
	[JsonPropertyName("price")]
	public int Price { get; set; }

	/// <summary>
	/// Цена билета без скидки.
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
	public string? Sector { get; set; }

	/// <summary>
	/// Идентификатор сектора.
	/// </summary>
	[JsonPropertyName("sector_id")]
	public int? SectorId { get; set; }

	/// <summary>
	/// Идентификатор места.
	/// </summary>
	[JsonPropertyName("place_id")]
	public int? PlaceId { get; set; }

	/// <summary>
	/// Номер ряда.
	/// </summary>
	[JsonPropertyName("row")]
	public string? Row { get; set; }

	/// <summary>
	/// Номер места.
	/// </summary>
	[JsonPropertyName("place")]
	public string? Place { get; set; }

	/// <summary>
	/// Штрихкод.
	/// </summary>
	[JsonPropertyName("barcode")]
	public string? Barcode { get; set; }

	/// <summary>
	/// Данные организатора.
	/// </summary>
	[JsonPropertyName("organizer")]
	public Organizer? Organizer { get; set; }
}
