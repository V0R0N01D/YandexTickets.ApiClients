using System.Text.Json;
using System.Text;
using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services;
using YandexTickets.ApiClients.Common.Services.Exceptions;
using YandexTickets.ApiClients.Common.Models.Responses;
using System.Net;

namespace YandexTickets.ApiClients.Common;

/// <summary>
/// Базовый класс для клиентов.
/// </summary>
public abstract class YandexTicketsApiClientBase
{
    /// <summary>
    /// Стандартные настройки для десериализации.
    /// </summary>
    private readonly JsonSerializerOptions _defaultJsonOptions = new();

    /// <summary>
    /// Клиент используемый для запросов.
    /// </summary>
    private readonly HttpClient _httpClient;

    private readonly AuthService _authService;

    /// <summary>
    /// Инициализирует новый экземпляр класса с указанным HTTP-клиентом.
    /// </summary>
    /// <param name="login">Логин для создания ключа авторизации.</param>
    /// <param name="password">Пароль для создания ключа авторизации.</param>
    /// <param name="httpClient">Экземпляр клиента для выполнения HTTP-запросов.</param>
    protected YandexTicketsApiClientBase(string login, string password, HttpClient httpClient)
    {
        _httpClient = httpClient;

        _authService = new AuthService(login, password);
    }

    /// <summary>
    /// Выполняет GET-запрос и возвращает десериализованный ответ.
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого ответа,
    /// который должен наследовать от <see cref="ResponseBase"/>.</typeparam>
    /// <param name="requestPath">Путь запроса с параметрами.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Десериализованный ответ от API.</returns>
    protected async Task<TResponse> SendGetRequestAsync<TResponse>(string requestPath,
        CancellationToken cancellationToken) where TResponse : ResponseBase

    {
        var response = await _httpClient.GetAsync(requestPath, cancellationToken)
            .ConfigureAwait(false);

        return await ProcessResponse<TResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет POST-запрос и возвращает десериализованный ответ.
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого ответа,
    /// который должен наследовать от <see cref="ResponseBase"/>.</typeparam>
    /// <param name="requestPath">Путь запроса с параметрами.</param>
    /// <param name="content">Содержимое тела запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Десериализованный ответ от API.</returns>
    protected async Task<TResponse> SendPostRequestAsync<TResponse>(string requestPath,
        HttpContent? content,
        CancellationToken cancellationToken) where TResponse : ResponseBase
    {
        var response = await _httpClient.PostAsync(requestPath, content, cancellationToken)
            .ConfigureAwait(false);

        return await ProcessResponse<TResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Обрабатывает HTTP-ответ, проверяя на наличие ошибок,
    /// и десериализует его в объект типа <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого ответа,
    /// который должен наследовать от <see cref="ResponseBase"/>.</typeparam>
    /// <param name="responseMessage">Сообщение HTTP-ответа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Десериализованный объект ответа типа <typeparamref name="TResponse"/>.</returns>
    private async Task<TResponse> ProcessResponse<TResponse>(HttpResponseMessage responseMessage,
        CancellationToken cancellationToken) where TResponse : ResponseBase
    {
        await using var stream = await responseMessage.Content.ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        var response = await CheckResponse<TResponse>(responseMessage.StatusCode, stream,
            cancellationToken);

        return response ?? await DeserializeResponseAsync<TResponse>(stream, cancellationToken);
    }

    /// <summary>
    /// Проверяет HTTP-ответ на наличие ошибок сервера и
    /// возвращает объект ответа с информацией об ошибке, если это необходимо.
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого ответа,
    /// который должен наследовать от <see cref="ResponseBase"/>.</typeparam>
    /// <param name="statusCode">Код состояния HTTP-ответа.</param>
    /// <param name="stream">Поток, содержащий данные ответа.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Объект типа <typeparamref name="TResponse"/> с заполненными полями ошибки, если обнаружена ошибка сервера; иначе — <c>null</c>.
    /// </returns>
    protected virtual async Task<TResponse?> CheckResponse<TResponse>(HttpStatusCode statusCode,
        Stream stream, CancellationToken cancellationToken) where TResponse : ResponseBase
    {
        if (statusCode is HttpStatusCode.InternalServerError)
        {
            // Чтение начала ответа, если он содержит <html тег то возврат стандартной ошибки
            var buffer = new byte[32];
            var bytesRead = await stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
            var initialContent = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (initialContent.Contains("<html"))
                return GetResponseWithError<TResponse>();
        }

        return null!;
    }

    /// <summary>
    /// Десериализует ответ, полученный в запросе.
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
    /// <param name="stream">Стрим с ответом.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Десериализованный ответ.</returns>
    private async Task<TResponse> DeserializeResponseAsync<TResponse>(Stream stream,
        CancellationToken cancellationToken)
    {
        var jsonOptions = GetSerializerOptions(typeof(TResponse));

        stream.Position = 0;

        var result = await JsonSerializer
            .DeserializeAsync<TResponse>(stream, jsonOptions, cancellationToken).ConfigureAwait(false);
        return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
    }

    /// <summary>
    /// Получает строку запроса из класса запроса.
    /// </summary>
    /// <param name="request">Класс запроса.</param>
    /// <returns>Строка запроса.</returns>
    protected string GetRequestString(RequestBase request)
    {
        request.Auth = _authService.GetAuthToken();
        return request.GetRequestPath();
    }

    /// <summary>
    /// Создает объект ответа типа <typeparamref name="TResponse"/> с информацией об ошибке.
    /// </summary>
    /// <typeparam name="TResponse">Тип ожидаемого ответа,
    /// который должен наследовать от <see cref="ResponseBase"/>.</typeparam>
    /// <param name="error">Сообщение об ошибке.
    /// Если не указано, используется "Ошибка выполнения на сервере.".</param>
    /// <returns>Объект ответа типа <typeparamref name="TResponse"/> с заполненными полями ошибки.</returns>
    protected static TResponse GetResponseWithError<TResponse>(string? error = null)
    {
        var response = Activator.CreateInstance<TResponse>();
        var statusProperty = typeof(TResponse).GetProperty("Status");
        var errorProperty = typeof(TResponse).GetProperty("Error");

        statusProperty!.SetValue(response, ResponseStatus.Failed);
        errorProperty!.SetValue(response, error ?? "Ошибка выполнения на сервере.");

        return response;
    }

    /// <summary>
    /// Получает настройки сериализации для указанного типа ответа.
    /// </summary>
    /// <param name="responseType">Тип ответа, для которого требуются настройки сериализации.</param>
    /// <returns>Настройки JSON-сериализатора.</returns>
    protected virtual JsonSerializerOptions GetSerializerOptions(Type responseType)
    {
        return _defaultJsonOptions;
    }
}