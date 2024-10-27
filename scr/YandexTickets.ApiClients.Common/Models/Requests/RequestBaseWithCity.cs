using YandexTickets.ApiClients.Common.Services.Attributes;

namespace YandexTickets.ApiClients.Common.Models.Requests;

/// <summary>
/// Класс запроса дополнительно содержащий идентификатор города.
/// </summary>
public abstract class RequestBaseWithCity : RequestBase
{
	/// <summary>
	/// Конструктор для создания запроса с идентификатором города.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	public RequestBaseWithCity(string auth, int cityId) : base(auth)
	{
		CityId = cityId;
	}

	/// <summary>
	/// Идентификатор города.
	/// </summary>
	[QueryParameter("city_id")]
	public int CityId { get; set; }
}
