namespace YandexTickets.Common.Services.Converters.Request;

public class EnumConverter : IQueryParameterConverter
{
	public string Convert(object value)
	{
		var enumValue = (Enum)value;
		return System.Convert.ToInt32(enumValue).ToString();
	}
}
