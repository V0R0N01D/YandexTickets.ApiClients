using Microsoft.AspNetCore.WebUtilities;
using System.Reflection;
using YandexTickets.Common.Services.Attributes;
using YandexTickets.Common.Services.Converters.Request;

namespace YandexTickets.Common.Models.Requests;

/// <summary>
/// Базовый запрос содержащий идентификатор внешней системы и action.
/// </summary>
public abstract class RequestBase
{
	private static readonly Dictionary<Type, List<PropertyMetadata>> _cachedProperties = new();

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
		if (!_cachedProperties.TryGetValue(type, out var propertiesMetadata))
		{
			propertiesMetadata = GetPropertiesMetadata(type);
			_cachedProperties[type] = propertiesMetadata;
		}

		Dictionary<string, string> queryParams = new()
		{
			{ "action", Action }
		};

		foreach (var metadata in propertiesMetadata)
		{
			var value = metadata.PropertyInfo.GetValue(this);
			AddQueryParam(queryParams, metadata.Attribute.Title, value,
				metadata.Attribute.IsRequired, metadata.Converter);
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
	/// <param name="converter">Конвертер для обработки значения параметра</param>
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

	/// <summary>
	/// Получает метаданные свойств для заданного типа и кэширует их.
	/// </summary>
	/// <param name="type">Тип запроса</param>
	/// <returns>Список метаданных свойств.</returns>
	private List<PropertyMetadata> GetPropertiesMetadata(Type type)
	{
		var propertiesMetadata = new List<PropertyMetadata>();

		var properties = type.GetProperties();

		foreach (var property in properties)
		{
			var attribute = property.GetCustomAttribute<QueryParameterAttribute>();
			if (attribute is null)
				continue;

			var converterAttribute = property.GetCustomAttribute<QueryParameterConverterAttribute>();
			var converter = GetConverter(converterAttribute?.ConverterType);

			propertiesMetadata.Add(new PropertyMetadata
			{
				PropertyInfo = property,
				Attribute = attribute,
				Converter = converter
			});
		}

		return propertiesMetadata;
	}

	/// <summary>
	/// Класс для хранения метаданных свойств.
	/// </summary>
	private class PropertyMetadata
	{
		public required PropertyInfo PropertyInfo { get; set; }
		public required QueryParameterAttribute Attribute { get; set; }
		public IQueryParameterConverter? Converter { get; set; }
	}
}
