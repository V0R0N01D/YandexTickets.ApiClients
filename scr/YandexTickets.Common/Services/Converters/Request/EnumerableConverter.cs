using System.Collections;

namespace YandexTickets.Common.Services.Converters.Request;

public class EnumerableConverter : IQueryParameterConverter
{
	public string Convert(object value)
	{
		var enumerable = (IEnumerable)value;
		var values = new List<string?>();

		foreach (var item in enumerable)
		{
			if (item != null)
				values.Add(item.ToString());
		}

		return string.Join(',', values);
	}
}
