using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using YandexTickets.Common.Services.Attributes;
using YandexTickets.Common.Services.Converters.Request;
using YandexTickets.Common.Services.Exceptions;

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
	/// Построить строку запроса с параметрами на основе класса запроса.
	/// </summary>
	/// <returns>Cтрока запроса с параметрами.</returns>
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

			var converterAttribute = property.GetCustomAttribute<QueryParameterConverterAttribute>();
			var converter = GetConverter(converterAttribute?.ConverterType);

			AddQueryParam(queryParams, attribute.Title, property.GetValue(this),
				attribute.IsRequired, converter);
		}

		return QueryHelpers.AddQueryString("?", queryParams!);
	}

	/// <summary>
	/// Создание конвертера
	/// </summary>
	/// <param name="converterType">Тип конвертера</param>
	/// <returns>Конвертор</returns>
	private IQueryParameterConverter? GetConverter(Type? converterType)
	{
		if (converterType != null)
			return (IQueryParameterConverter)Activator.CreateInstance(converterType)!;

		return null;
	}

	/// <summary>
	/// Добавляет параметр в запрос.
	/// </summary>
	/// <param name="dictionary">Словарь с параметрами</param>
	/// <param name="title">Название параметра</param>
	/// <param name="value">Значение параметра</param>
	/// <param name="isRequired">Является ли параметр обязательным</param>
	/// <param name="converterType">Конвертер для обработки значения параметра</param>
	private static void AddQueryParam(Dictionary<string, string> dictionary, string title,
		object? value, bool isRequired = false, IQueryParameterConverter? converter = null)
	{
		if (value is null)
		{
			if (isRequired)
				throw new ArgumentNullException($"Обязательный параметр {title} равен null.");

			return;
		}

		string? strValue = converter is null
			? value.ToString()
			: converter.Convert(value);

		if (string.IsNullOrWhiteSpace(strValue))
		{
			if (isRequired)
				throw new ArgumentException($"Обязательный параметр {title} содержит пустую строку.");

			return;
		}

		dictionary.TryAdd(title, strValue);
	}
}
