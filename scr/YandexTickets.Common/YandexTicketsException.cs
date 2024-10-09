namespace YandexTickets.Common;

/// <summary>
/// Кастомное исключение для ошибок связанных с API Яндекс.Билетов.
/// </summary>
public class YandexTicketsException : Exception
{
	public YandexTicketsException() { }
	public YandexTicketsException(string message) : base(message) { }
	public YandexTicketsException(string message, Exception innerException) 
		: base(message, innerException) { }
}
