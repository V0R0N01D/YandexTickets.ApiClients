namespace YandexTickets.ApiClients.Common.Services.Converters.Request;

/// <summary>
/// Конвертер для преобразования типа DateOnly в строку формата "yyyy-MM-dd"
/// для передачи параметра в запросе.
/// </summary>
public class DateOnlyConverter : IQueryParameterConverter
{
	public string Convert(object value)
	{
		if (value is not DateOnly date)
			throw new ArgumentException("Ожидался тип DateOnly.", nameof(value));

		return date.ToString("yyyy-MM-dd");
	}
}
