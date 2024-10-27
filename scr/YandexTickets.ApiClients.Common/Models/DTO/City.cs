using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Common.Models.DTO;

/// <summary>
/// Город.
/// </summary>
public record City
{
	/// <summary>
	/// Идентификатор города.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public required int Id { get; set; }

	/// <summary>
	/// Название города.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }
}
