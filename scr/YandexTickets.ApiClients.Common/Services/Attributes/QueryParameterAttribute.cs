namespace YandexTickets.ApiClients.Common.Services.Attributes;

/// <summary>
/// Атрибут для задания пользовательского названия параметра запроса 
/// и указания является ли параметр обязательным.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class QueryParameterAttribute : Attribute
{
	/// <summary>
	/// Название параметра запроса.
	/// </summary>
	public string Title { get; }

	/// <summary>
	/// Является ли параметр обязательным.
	/// </summary>
	public bool IsRequired { get; }

	/// <summary>
	/// Инициализирует новый экземпляр атрибута с заданным именем параметра.
	/// </summary>
	/// <param name="title">Название параметра запроса.</param>
	/// <param name="isRequired">Является ли параметр обязательным. (По умолчанию true)</param>
	public QueryParameterAttribute(string title, bool isRequired = true)
	{
		Title = title;
		IsRequired = isRequired;
	}
}

