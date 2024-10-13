using System.Net.Http.Json;
using YandexTickets.Common.Services.Exceptions;

namespace YandexTickets.Common;

public abstract class YandexTicketsApiClientBase
{
	protected readonly HttpClient _httpClient;

	protected YandexTicketsApiClientBase(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	/// <summary>
	/// Выполняет GET-запрос и возвращает десериализованный ответ.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="requestPath">Путь запроса с параметрами.</param>
	/// <returns>Десериализованный ответ от API.</returns>
	protected async Task<TResponse> SendGetRequestAsync<TResponse>(string requestPath,
		CancellationToken ct)
	{
		var response = await _httpClient.GetAsync(requestPath);
		response.EnsureSuccessStatusCode();

		return await DeserializeResponseAsync<TResponse>(response.Content, ct);
	}

	/// <summary>
	/// Десериализует ответ полученный в запросе.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="content">Содержимое ответа.</param>
	/// <returns>Десериализованный ответ.</returns>
	protected virtual async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpContent content,
		CancellationToken ct)
	{
		var result = await content.ReadFromJsonAsync<TResponse>(ct);
		return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
	}
}
