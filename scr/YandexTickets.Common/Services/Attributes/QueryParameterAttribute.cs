namespace YandexTickets.Common.Services.Attributes;

/// <summary>
/// Атрибут для задания пользовательского имени параметра запроса и указания является ли параметр обязательным.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class QueryParameterAttribute : Attribute
{
    /// <summary>
    /// Имя параметра запроса.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Является ли параметр обязательным.
    /// </summary>
    public bool IsRequired { get; }

	/// <summary>
	/// Инициализирует новый экземпляр атрибута с заданным именем параметра.
	/// </summary>
	/// <param name="name">Имя параметра запроса.</param>
	/// <param name="isRequired">Является ли параметр обязательным. (По умолчанию true)</param>
	public QueryParameterAttribute(string name, bool isRequired = true)
    {
        Name = name;
        IsRequired = isRequired;
    }
}

