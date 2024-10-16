﻿using YandexTickets.Common.Models.Requests;
using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Requests;

/// <summary>
/// Класс запроса для получения списка агентов.
/// </summary>
public class GetAgentListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка агентов.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="limit">Максимальное количество возвращаемых объектов. Выдается не больше 1000 агентов.</param>
	/// <param name="offset">Количество пропускаемых в ответе объектов, начиная с первого.</param>
	public GetAgentListRequest(string auth, string cityId, int? limit = null, int? offset = null)
		: base(auth, cityId)
	{
		Limit = limit;
		Offset = offset;
	}

	protected override string Action => "crm.agent.list";

	/// <summary>
	/// Максимальное количество возвращаемых объектов. Выдается не больше 1000 агентов.
	/// </summary>
	[QueryParameter("limit", false)]
	public int? Limit { get; set; }

	/// <summary>
	/// Количество пропускаемых в ответе объектов, начиная с первого.
	/// </summary>
	[QueryParameter("offset", false)]
	public int? Offset { get; set; }
}
