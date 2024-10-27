using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Concurrent;
using System.Reflection;
using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;
using YandexTickets.ApiClients.Common.Services.Converters.Request;

namespace YandexTickets.ApiClients.Common.Services;

/// <summary>
/// Класс для построения строки запроса на основе данных из запроса.
/// </summary>
internal static class RequestPathBuilder
{
	private static readonly ConcurrentDictionary<Type, List<PropertyMetadata>> _cachedProperties = new();

	/// <summary>
	/// Строит строку запроса с параметрами на основе экземпляра запроса.
	/// </summary>
	/// <param name="request">Экземпляр запроса.</param>
	/// <returns>Строка запроса с параметрами.</returns>
	internal static string GetRequestPath(this RequestBase request)
	{
		var type = request.GetType();
		if (!_cachedProperties.TryGetValue(type, out var propertiesMetadata))
		{
			propertiesMetadata = GetPropertiesMetadata(type);
			_cachedProperties[type] = propertiesMetadata;
		}

		Dictionary<string, string> queryParams = new()
		{
			{ "action", request.Action }
		};

		foreach (var metadata in propertiesMetadata)
		{
			var value = metadata.PropertyInfo.GetValue(request);
			AddQueryParam(queryParams, metadata.Attribute.Title, value,
				metadata.Attribute.IsRequired, metadata.Converter);
		}

		return QueryHelpers.AddQueryString("?", queryParams!);
	}

	/// <summary>
	/// Создает конвертера.
	/// </summary>
	/// <param name="converterType">Тип конвертера.</param>
	/// <returns>Конвертер.</returns>
	private static IQueryParameterConverter? GetConverter(Type? converterType)
	{
		if (converterType != null)
			return (IQueryParameterConverter)Activator.CreateInstance(converterType)!;

		return null;
	}

	/// <summary>
	/// Добавляет параметр в запрос.
	/// </summary>
	/// <param name="dictionary">Словарь с параметрами.</param>
	/// <param name="title">Название параметра.</param>
	/// <param name="value">Значение параметра.</param>
	/// <param name="isRequired">Является ли параметр обязательным.</param>
	/// <param name="converter">Конвертер для обработки значения параметра.</param>
	private static void AddQueryParam(Dictionary<string, string> dictionary, string title,
		object? value, bool isRequired = false, IQueryParameterConverter? converter = null)
	{
		if (value is null)
		{
			if (isRequired)
				throw new ArgumentNullException(title, $"Обязательный параметр {title} равен null.");

			return;
		}

		string? strValue = converter is null
			? value.ToString()
			: converter.Convert(value);

		if (string.IsNullOrWhiteSpace(strValue))
		{
			if (isRequired)
				throw new ArgumentException($"Обязательный параметр {title} содержит пустую строку.", title);

			return;
		}

		dictionary.TryAdd(title, strValue);
	}

	/// <summary>
	/// Получает метаданные свойств для заданного типа.
	/// </summary>
	/// <param name="type">Тип запроса.</param>
	/// <returns>Список метаданных свойств.</returns>
	private static List<PropertyMetadata> GetPropertiesMetadata(Type type)
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
