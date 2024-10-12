using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using YandexTickets.Common;
using YandexTickets.CrmApiClient.Models.Requests;
using YandexTickets.CrmApiClient.Models.Responses;
using YandexTickets.CrmApiClient.Services.Attributes;
using YandexTickets.CrmApiClient.Services.Converters;

namespace YandexTickets.CrmApiClient;

/// <summary>
/// Клиент для взаимодействия с API CRM Яндекс.Билетов.
/// </summary>
public class YandexTicketsCrmApiClient : IYandexTicketsCrmApiClient
{
	private string _baseUrl = "https://api.tickets.yandex.net/api/crm/";

	/// <summary>
	/// Хост для всех запросов к API. <br><br></br></br>
	/// Источник: <a href="https://yandex.ru/dev/tickets/crm/doc/ru/concepts/access"/>.
	/// </summary>
	public string BaseURL
	{
		get => _baseUrl;
		set
		{
			_baseUrl = value;
			_httpClient.BaseAddress = new Uri(_baseUrl);
		}
	}

	private readonly HttpClient _httpClient;

	/// <summary>
	/// Конструктор клиента для взаимодействия с API CRM Яндекс.Билетов.
	/// </summary>
	/// <param name="client">HttpClient который будет использоваться для запросов.</param>
	public YandexTicketsCrmApiClient(HttpClient client)
	{
		_httpClient = client;
		client.BaseAddress = new Uri(_baseUrl);
	}

	/// <summary>
	/// Возвращает массив городов.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт CityListResponse,
	/// содержащий статус ответа и список городов или ошибку.
	/// При внутренней ошибке клиента выкинет YandexTicketsException.
	/// </returns>
	public Task<CityListResponse> GetCityListAsync(GetCityListRequest request)
	{
		return SendGetRequestAsync<CityListResponse>(request.GetRequestPath());
	}

	/// <summary>
	/// Возвращает массив мероприятий.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт ActivityListResponse,
	/// содержащий статус ответа и список мероприятий или ошибку.
	/// При внутренней ошибке клиента выкинет YandexTicketsException.
	/// </returns>
	public Task<ActivityListResponse> GetActivityListAsync(GetActivityListRequest request)
	{
		return SendGetRequestAsync<ActivityListResponse>(request.GetRequestPath());
	}

	/// <summary>
	/// Возвращает список всех событий. 
	/// Чтобы получить список всех событий (сеансов) внутри конкретного мероприятия, 
	/// нужно передать параметр ActivityId в GetEventListRequest. 
	/// Чтобы получить информацию по конкретному событию, 
	/// нужно передать параметр EventId в GetEventListRequest.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <returns>
	/// При наличии ответа, вернёт EventListResponse,
	/// содержащий статус ответа и список событий или ошибку.
	/// При внутренней ошибке клиента выкинет YandexTicketsException.
	/// </returns>
	public Task<EventListResponse> GetEventListAsync(GetEventListRequest request)
	{
		return SendGetRequestAsync<EventListResponse>(request.GetRequestPath());
	}


	/// <summary>
	/// Выполняет GET-запрос и возвращает десериализованный ответ.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="requestPath">Путь запроса с параметрами.</param>
	/// <returns>Десериализованный ответ от API.</returns>
	/// <exception cref="YandexTicketsException">Выбрасывается при ошибке десериализации или пустом ответе.</exception>
	private async Task<TResponse> SendGetRequestAsync<TResponse>(string requestPath) // where TResponse : ResponseBase<> Это не работает
	{
		var response = await _httpClient.GetAsync(requestPath);
		response.EnsureSuccessStatusCode();

		return await DeserializeResponse<TResponse>(response.Content);
	}

	/// <summary>
	/// Десериализует ответ полученный в запросе.
	/// </summary>
	/// <typeparam name="TResponse">Тип ожидаемого ответа.</typeparam>
	/// <param name="content">Содержимое ответа.</param>
	/// <returns>Десериализованный ответ.</returns>
	/// <exception cref="YandexTicketsException"></exception>
	private async Task<TResponse> DeserializeResponse<TResponse>(HttpContent content)
	{
		try
		{
			var options = new JsonSerializerOptions();

			// Проверка есть ли у класса ответ атрибут,
			// при котором будет иная обработка десериализации массива
			var type = typeof(TResponse);
			var brokenAttribute = type.GetCustomAttribute<SingleElementArrayAttribute>();
			if (brokenAttribute != null)
				options.Converters.Add(new SingleElementArrayConverterFactory());

			var result = await content.ReadFromJsonAsync<TResponse>(options, CancellationToken.None);
			return result ?? throw new YandexTicketsException("Получен пустой ответ от сервера.");
		}
		catch (JsonException ex)
		{
			throw new YandexTicketsException("Ошибка при десериализации ответа от сервера.", ex);
		}
	}
}
