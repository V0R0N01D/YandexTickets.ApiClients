using YandexTicketsCommon.Services.Attributes;

namespace YandexTicketsCommon.Models.Requests;

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
	public RequestBaseWithCity(string auth, string cityId) : base(auth)
	{
		CityId = cityId;
	}

	/// <summary>
	/// Идентификатор города.
	/// </summary>
	[QueryParameter("city_id")]
	public string CityId { get; set; }
}
