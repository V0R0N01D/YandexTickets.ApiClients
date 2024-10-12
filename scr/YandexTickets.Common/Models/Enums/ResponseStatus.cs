using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace YandexTickets.Common.Models.Enums;

/// <summary>
/// Статус ответа.
/// Значения:
/// Success - Запрос выполнен;
/// Failed - Произошла ошибка
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
