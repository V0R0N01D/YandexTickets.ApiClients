namespace YandexTickets.Common.Services.Converters.Request;

public class DateOnlyConverter : IQueryParameterConverter
{
	public string Convert(object value)
	{
		var date = (DateOnly)value;
		return date.ToString("yyyy-MM-dd");
	}
}
