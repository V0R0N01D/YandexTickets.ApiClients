using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Common.Models.Enums;

/// <summary>
/// Статус ответа. <br/><br/>
/// Значения: <br/>
/// Success - Запрос выполнен; <br/>
/// Failed - Произошла ошибка <br/>
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResponseStatus
{
	/// <summary>
	/// Запрос выполнен
	/// </summary>
	Success,

	/// <summary>
	/// Произошла ошибка
	/// </summary>
	Failed,
}
