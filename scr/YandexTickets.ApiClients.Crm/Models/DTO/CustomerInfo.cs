using System.Text.Json.Serialization;
using YandexTickets.ApiClients.Crm.Models.Enums;

namespace YandexTickets.ApiClients.Crm.Models.DTO;

/// <summary>
/// Данные покупателя (с заказами).
/// </summary>
public record CustomerInfo
{
	/// <summary>
	/// Идентификатор покупателя.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// ФИО покупателя.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Телефон покупателя.
	/// </summary>
	[JsonPropertyName("phone")]
	public string? Phone { get; set; }

	/// <summary>
	/// Email покупателя.
	/// </summary>
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	/// <summary>
	/// Согласен ли покупатель на получение рассылок.
	/// </summary>
	[JsonPropertyName("is_subscripted")]
	public CustomerConsent IsSubscripted { get; set; }

	/// <summary>
	/// Массив идентификаторов заказов покупателя.
	/// </summary>
	[JsonPropertyName("orders")]
	public required List<string> Orders { get; set; }
}
