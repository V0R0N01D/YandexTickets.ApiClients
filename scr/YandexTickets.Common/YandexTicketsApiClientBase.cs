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
		CancellationToken cancellationToken)
	{
		var response = await _httpClient.GetAsync(requestPath);
		response.EnsureSuccessStatusCode();

		return await DeserializeResponseAsync<TResponse>(response.Content, cancellationToken);
	}

	/// <summary>
	/// Выполняет POST-запрос и возвращает десериализованный ответ.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="requestPath">Путь запроса с параметрами.</param>
	/// <returns>Десериализованный ответ от API.</returns>
	protected async Task<TResponse> SendPostRequestAsync<TResponse>(string requestPath,
		HttpContent? content,
		CancellationToken cancellationToken)
	{
		var response = await _httpClient.PostAsync(requestPath, content, cancellationToken);
		response.EnsureSuccessStatusCode();

		return await DeserializeResponseAsync<TResponse>(response.Content, cancellationToken);
	}

	/// <summary>
	/// Десериализует ответ полученный в запросе.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="content">Содержимое ответа.</param>
	/// <returns>Десериализованный ответ.</returns>
	protected virtual async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpContent content,
		CancellationToken cancellationToken)
	{
		var result = await content.ReadFromJsonAsync<TResponse>(cancellationToken);
		return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
	}
}
