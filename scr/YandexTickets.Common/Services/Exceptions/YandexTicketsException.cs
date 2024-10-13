namespace YandexTickets.Common.Services.Exceptions;

/// <summary>
/// Кастомное исключение для ошибок связанных с клиентом API Яндекс.Билетов.
/// </summary>
public class YandexTicketsException : Exception
{
	public YandexTicketsException() { }
	public YandexTicketsException(string message) : base(message) { }
	public YandexTicketsException(string message, Exception innerException)
		: base(message, innerException) { }
}
