using System.Collections;

namespace YandexTickets.Common.Services.Converters.Request;

/// <summary>
/// Конвертер для преобразования IEnumerable (списков) в строку 
/// с разделителями для передачи параметров в запросе.
/// </summary>
public class EnumerableConverter : IQueryParameterConverter
{
	public string Convert(object value)
	{
		if (value is not IEnumerable enumerable)
			throw new ArgumentException("Ожидался тип IEnumerable.", nameof(value));

		var values = new List<string?>();

		foreach (var item in enumerable)
		{
			if (item != null)
				values.Add(item.ToString());
		}

		return string.Join(',', values);
	}
}
