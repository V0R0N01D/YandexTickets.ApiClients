using System.Text.Json.Serialization;
using YandexTickets.ApiClients.Crm.Models.Enums;

namespace YandexTickets.ApiClients.Crm.Models.DTO;

/// <summary>
/// Данные покупателя.
/// </summary>
public record Customer
{
	/// <summary>
	/// Идентификатор покупателя.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int Id { get; set; }

	/// <summary>
	/// Имя покупателя.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Телефон покупателя.
	/// </summary>
	[JsonPropertyName("phone")]
	public string? Phone { get; set; }

	/// <summary>
	/// Электронная почта покупателя.
	/// </summary>
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	/// <summary>
	/// Согласен ли покупатель на получение рассылок.
	/// </summary>
	[JsonPropertyName("is_subscripted")]
	public CustomerConsent IsSubscripted { get; set; }
}
