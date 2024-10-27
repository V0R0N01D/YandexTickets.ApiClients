namespace YandexTickets.ApiClients.Common.Services.Converters.Request;

/// <summary>
/// Интерфейс для создания конвертеров параметров запроса.
/// </summary>
public interface IQueryParameterConverter
{
	/// <summary>
	/// Преобразует значение параметра в строковое представление для запроса.
	/// </summary>
	/// <param name="value">Значение параметра.</param>
	/// <returns>Строковое представление значения параметра для запроса.</returns>
	string Convert(object value);
}
