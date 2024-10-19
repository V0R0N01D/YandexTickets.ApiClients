namespace YandexTickets.Common.Services.Converters.Request;

/// <summary>
/// Конвертер для преобразования значений перечислений (Enum) в числовое представление для запроса.
/// </summary>
public class EnumConverter : IQueryParameterConverter
{
	public string Convert(object value)
	{
		if (value is not Enum enumValue)
			throw new ArgumentException("Ожидался тип Enum.", nameof(value));

		return System.Convert.ToInt32(enumValue).ToString();
	}
}
