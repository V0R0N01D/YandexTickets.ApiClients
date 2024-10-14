using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using System.Reflection;
using System.Text;
using YandexTickets.Common.Services.Attributes;
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

			string paramName = attribute.Name;

			AddQueryParam(queryParams, paramName, property.GetValue(this), attribute.IsRequired);
		}

		return QueryHelpers.AddQueryString("?", queryParams!);
	}

	/// <summary>
	/// Добавляет параметр в запрос.
	/// </summary>
	/// <param name="dictionary">Словарь с параметрами</param>
	/// <param name="title">Название параметра</param>
	/// <param name="value">Значение параметра</param>
	/// <param name="isRequired">Является ли параметр обязательным</param>
	private static void AddQueryParam(Dictionary<string, string> dictionary, string title,
		object? value, bool isRequired = false)
	{
		if (value is null)
		{
			if (isRequired)
				throw new ArgumentNullException($"Обязательный параметр {title} равен null.");

			return;
		}

		string? strValue;

		if (value is DateOnly dateOnlyValue)
		{
			// Форматирование DateOnly в строку формата "yyyy-MM-dd"
			strValue = dateOnlyValue.ToString("yyyy-MM-dd");
		}
		else if (value is Enum enumValue)
		{
			strValue = Convert.ToInt32(enumValue).ToString();
		}
		else if (value is IEnumerable enumerable && value is not string)
		{
			// Формирование строки в формате "value,value..."
			var values = new List<string?>();

			foreach (var item in enumerable)
			{
				if (item != null)
					values.Add(item.ToString());
			}

			strValue = string.Join(',', values);
		}
		else
		{
			strValue = value.ToString();
		}

		if (string.IsNullOrWhiteSpace(strValue))
		{
			if (isRequired)
				throw new ArgumentException($"Обязательный параметр {title} содержит пустую строку.");

			return;
		}

		dictionary.TryAdd(title, strValue);
	}
}
