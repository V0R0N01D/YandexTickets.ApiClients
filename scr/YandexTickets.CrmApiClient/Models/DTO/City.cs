using System.Text.Json.Serialization;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Город.
/// </summary>
public record City
{
	/// <summary>
	/// Идентификатор города.
	/// </summary>
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	/// <summary>
	/// Название города.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }
}
