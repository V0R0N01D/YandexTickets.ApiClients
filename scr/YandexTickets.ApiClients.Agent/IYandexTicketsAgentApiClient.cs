using YandexTickets.ApiClients.Agent.Models.Requests;
using YandexTickets.ApiClients.Agent.Models.Responses;

namespace YandexTickets.ApiClients.Agent;

public interface IYandexTicketsAgentApiClient
{
	/// <summary>
	/// Возвращает список городов.
	/// </summary>
	/// <param name="request">Объект который содержит данные для запроса.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>
	/// При наличии ответа, вернёт CityListResponse,
	/// содержащий статус ответа и список городов или ошибку.
	/// </returns>
	Task<CityListResponse> GetCityListAsync(GetCityListRequest request,
		CancellationToken cancellationToken = default);
}
