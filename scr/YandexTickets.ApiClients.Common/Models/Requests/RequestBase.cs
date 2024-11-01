﻿using YandexTickets.ApiClients.Common.Services.Attributes;

namespace YandexTickets.ApiClients.Common.Models.Requests;

/// <summary>
/// Базовый класс запроса, содержащий идентификатор внешней системы и действие.
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
