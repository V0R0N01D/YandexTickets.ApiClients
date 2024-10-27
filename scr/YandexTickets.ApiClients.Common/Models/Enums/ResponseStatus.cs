using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Common.Models.Enums;

/// <summary>
/// Статус ответа.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>Success</term>
/// <description>Запрос выполнен успешно</description>
/// </item>
/// <item>
/// <term>Failed</term>
/// <description>Произошла ошибка</description>
/// </item>
/// </list>
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResponseStatus
{
	/// <summary>
	/// Запрос выполнен успешно
	/// </summary>
	Success,

	/// <summary>
	/// Произошла ошибка
	/// </summary>
	Failed,
}
