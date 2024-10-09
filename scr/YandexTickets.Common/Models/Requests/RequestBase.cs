using Microsoft.AspNetCore.WebUtilities;
using System.Reflection;
using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.Common.Models.Requests;

/// <summary>
/// Базовый запрос содержащий идентификатор внешней системы и action.
/// </summary>
public abstract class RequestBase
{
	/// <summary>
	/// Конструктор базового запроса с указанием идентификатора внешней системы.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	public RequestBase(string auth)
	{
		Auth = auth;
	}

	/// <summary>
	/// Идентификатор внешней системы.
	/// </summary>
	[QueryParameter("auth")]
	public string Auth { get; set; }

	/// <summary>
	/// Действие, определяющее тип запроса к API (идентификатор запроса).
	/// </summary>
	protected abstract string Action { get; }

	/// <summary>
	/// Построить строку запроса с параметрами.
	/// </summary>
	public string GetRequestPath()
	{
		var type = this.GetType();
		var propeties = type.GetProperties();

		Dictionary<string, string> queryParams = new()
		{
			{ "action", Action }
		};

		foreach (var property in propeties)
		{
			var attribute = property.GetCustomAttribute<QueryParameterAttribute>();

			if (attribute is null)
				continue;

			string paramName = attribute.Name;

			if (attribute.IsRequired)
			{
				AddQueryParam(queryParams, paramName, property.GetValue(this));
				continue;
			}

			AddOptionalQueryParam(queryParams, paramName, property.GetValue(this));
		}

		return QueryHelpers.AddQueryString("?", queryParams!);
	}

	/// <summary>
	/// Добавляет обязательный параметр в запрос.
	/// </summary>
	/// <param name="dictionary">Словарь с параметрами</param>
	/// <param name="title">Название параметра</param>
	/// <param name="value">Значение параметра</param>
	/// <exception cref="YandexTicketsException"></exception>
	private static void AddQueryParam(Dictionary<string, string> dictionary, string title,
		object? value)
	{
		if (value is null)
			throw new YandexTicketsException($"Обязательный параметр {title} равен null.");

		var strValue = value.ToString();
		if (string.IsNullOrWhiteSpace(strValue))
			throw new YandexTicketsException($"Обязательный параметр {title} " +
				$"содержит пустую строку.");

		dictionary.TryAdd(title, strValue);
	}

	/// <summary>
	/// Добавляет необязательный параметр в запрос.
	/// </summary>
	/// <param name="dictionary">Словарь с параметрами</param>
	/// <param name="title">Название параметра</param>
	/// <param name="value">Значение параметра</param>
	private static void AddOptionalQueryParam(Dictionary<string, string> dictionary, string title,
		object? value)
	{
		if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
			return;

		AddQueryParam(dictionary, title, value);
	}
}
