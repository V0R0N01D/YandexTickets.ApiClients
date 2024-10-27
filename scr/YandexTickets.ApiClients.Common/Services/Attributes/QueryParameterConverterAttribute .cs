namespace YandexTickets.ApiClients.Common.Services.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class QueryParameterConverterAttribute : Attribute
{
	public Type ConverterType { get; }

	public QueryParameterConverterAttribute(Type converterType)
	{
		ConverterType = converterType;
	}
}
