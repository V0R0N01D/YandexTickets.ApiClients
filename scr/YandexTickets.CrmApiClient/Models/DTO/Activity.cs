using System.Text.Json.Serialization;
using YandexTickets.Common.Models.Enums;

namespace YandexTickets.CrmApiClient.Models.DTO;

/// <summary>
/// Мероприятие.
/// </summary>
public record Activity
{
	/// <summary>
	/// Идентификатор мероприятия.
	/// </summary>
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	/// <summary>
	/// Статус мероприятия.
	/// </summary>
	[JsonPropertyName("status")]
	public EventStatus Status { get; set; }

	/// <summary>
	/// Название мероприятия.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Описание мероприятия.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Возрастное ограничение.
	/// </summary>
	[JsonPropertyName("age")]
	public required string Age { get; set; }

	/// <summary>
	/// Массив идентификаторов жанров.
	/// </summary>
	[JsonPropertyName("genres_id")]
	public required string[] GenresId { get; set; }

	/// <summary>
	/// Идентификатор мероприятия КХЛ.
	/// </summary>
	[JsonPropertyName("khl_id")]
	public int? KhlId { get; set; }
}
