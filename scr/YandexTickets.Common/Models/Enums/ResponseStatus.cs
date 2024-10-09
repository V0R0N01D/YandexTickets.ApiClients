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
	[EnumMember(Value = "0")]
	Success,

	/// <summary>
	/// Произошла ошибка
	/// </summary>
	[EnumMember(Value = "1")]
	Failed,
}
