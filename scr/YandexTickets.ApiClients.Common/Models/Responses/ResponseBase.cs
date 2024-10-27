using System.Text.Json.Serialization;
using YandexTickets.ApiClients.Common.Models.Enums;

namespace YandexTickets.ApiClients.Common.Models.Responses;

/// <summary>
/// Базовый класс для ответов от API Яндекс.Билетов.
/// </summary>
public abstract class ResponseBase<T> : ResponseBase
{
	/// <summary>
	/// Результат выполнения запроса.
	/// </summary>
	[JsonPropertyName("result")]
	public T? Result { get; set; }
}


public abstract class ResponseBase
{
	/// <summary>
	/// Статус ответа от API.
	/// </summary>
	[JsonPropertyName("status")]
	public ResponseStatus Status { get; set; }

	/// <summary>
	/// Сообщение об ошибке, если она произошла.
	/// </summary>
	[JsonPropertyName("error")]
	public string? Error { get; set; }
}
