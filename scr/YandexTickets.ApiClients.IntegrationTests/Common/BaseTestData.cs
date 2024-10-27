namespace YandexTickets.ApiClients.IntegrationTests.Common;

// Базовый класс для тестовых данных
public abstract class BaseTestData
{
	public string? Login { get; set; }
	public string? Password { get; set; }
	public int? CityId { get; set; }
}
