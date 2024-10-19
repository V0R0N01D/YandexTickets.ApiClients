using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.Common.Models.Requests;

/// <summary>
/// Базовый запрос содержащий идентификатор внешней системы и action.
/// </summary>
public abstract class RequestBase
{
	/// <summary>
	/// Конструктор базового запроса с указанием идентификатора внешней системы.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	public RequestBase(string auth)
	{
		Auth = auth;
	}

	/// <summary>
	/// Идентификатор внешней системы.
	/// </summary>
	[QueryParameter("auth")]
	public string Auth { get; set; }

	/// <summary>
	/// Действие, определяющее тип запроса к API (идентификатор запроса).
	/// </summary>
	protected internal abstract string Action { get; }
}
