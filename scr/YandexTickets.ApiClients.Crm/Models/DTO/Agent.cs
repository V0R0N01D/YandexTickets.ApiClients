using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Crm.Models.DTO;

/// <summary>
/// Агент.
/// </summary>
public record Agent
{
	/// <summary>
	/// Идентификатор агента.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Название агента.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }
}
