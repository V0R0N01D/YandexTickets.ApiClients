using System.Net.Http.Json;
using YandexTickets.Common.Models.Requests;
using YandexTickets.Common.Services;
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
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Десериализованный ответ от API.</returns>
	protected async Task<TResponse> SendGetRequestAsync<TResponse>(string requestPath,
		CancellationToken cancellationToken)

	{
		var response = await _httpClient.GetAsync(requestPath, cancellationToken)
			.ConfigureAwait(false);
		response.EnsureSuccessStatusCode();

		return await DeserializeResponseAsync<TResponse>(response.Content, cancellationToken)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Выполняет POST-запрос и возвращает десериализованный ответ.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="requestPath">Путь запроса с параметрами.</param>
	/// <param name="content">Содержимое тела запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Десериализованный ответ от API.</returns>
	protected async Task<TResponse> SendPostRequestAsync<TResponse>(string requestPath,
		HttpContent? content,
		CancellationToken cancellationToken)
	{
		var response = await _httpClient.PostAsync(requestPath, content, cancellationToken)
			.ConfigureAwait(false);
		response.EnsureSuccessStatusCode();

		return await DeserializeResponseAsync<TResponse>(response.Content, cancellationToken)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Десериализует ответ, полученный в запросе.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="content">Содержимое ответа.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Десериализованный ответ.</returns>
	protected virtual async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpContent content,
		CancellationToken cancellationToken)

	{
		var result = await content.ReadFromJsonAsync<TResponse>(cancellationToken).ConfigureAwait(false);
		return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
	}

	/// <summary>
	/// Получение строки запроса из класса запроса
	/// </summary>
	/// <param name="request">Класс запроса</param>
	/// <returns>Строка запроса</returns>
	protected string GetRequestString(RequestBase request)
	{
		return request.GetRequestPath();
	}
}
