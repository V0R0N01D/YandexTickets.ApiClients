using System.Text.Json.Serialization;
using YandexTickets.Common.Models.Enums;
using YandexTickets.Common.Services.Converters;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Событие.
/// </summary>
public record Event
{
	/// <summary>
	/// Идентификатор события.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Статус события.
	/// </summary>
	[JsonPropertyName("status")]
	public EventStatus Status { get; set; }

	/// <summary>
	/// Название события.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Идентификатор места проведения.
	/// </summary>
	[JsonPropertyName("venue_id")]
	public int VenueId { get; set; }

	/// <summary>
	/// Идентификатор версии зала.
	/// </summary>
	[JsonPropertyName("version_id")]
	public int VersionId { get; set; }

	/// <summary>
	/// Дата события.
	/// </summary>
	[JsonPropertyName("date")]
	[JsonConverter(typeof(YandexDateTimeConverter))]
	public DateTime Date { get; set; }

	/// <summary>
	/// Идентификатор мероприятия.
	/// </summary>
	[JsonPropertyName("activity_id")]
	public int ActivityId { get; set; }

	/// <summary>
	/// Идентификатор города.
	/// </summary>
	[JsonPropertyName("city_id")]
	public int CityId { get; set; }

	/// <summary>
	/// Количество часов до события, когда прекращается продажа билетов.
	/// </summary>
	[JsonPropertyName("close_to")]
	public int CloseTo { get; set; }

	/// <summary>
	/// Идентификатор мероприятия КХЛ.
	/// </summary>
	[JsonPropertyName("khl_id")]
	public int? KhlId { get; set; }
}
