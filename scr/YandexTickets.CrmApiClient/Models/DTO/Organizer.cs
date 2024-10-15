using System.Text.Json.Serialization;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Данные организатора для печати на билете.
/// </summary>
public record Organizer
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	/// <summary>
	/// Юридическое название организации.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Юридический адрес организации.
	/// </summary>
	[JsonPropertyName("address")]
	public string? Address { get; set; }

	/// <summary>
	/// ИНН организации.
	/// </summary>
	[JsonPropertyName("inn")]
	public string? Inn { get; set; }
}
