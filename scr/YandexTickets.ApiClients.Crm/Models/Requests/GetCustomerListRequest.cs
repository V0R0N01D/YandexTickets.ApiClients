﻿using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

/// <summary>
/// Класс запроса для получения списка покупателей.
/// </summary>
public class GetCustomerListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка покупателей.
	/// </summary>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="limit">Максимальное количество возвращаемых объектов. Выдается не больше 1000 покупателей.</param>
	/// <param name="offset">Количество пропускаемых в ответе объектов, начиная с первого.</param>
	public GetCustomerListRequest(int cityId, int? limit = null, int? offset = null)
		: base(cityId)
	{
		Limit = limit;
		Offset = offset;
	}

	/// <inheritdoc />
	protected override string Action => "crm.customer.list";

	/// <summary>
	/// Максимальное количество возвращаемых объектов.
	/// </summary>
	/// <remarks>Выдается не больше 1000 покупателей.</remarks>
	[QueryParameter("limit", false)]
	public int? Limit { get; set; }

	/// <summary>
	/// Количество пропускаемых в ответе объектов, начиная с первого.
	/// </summary>
	[QueryParameter("offset", false)]
	public int? Offset { get; set; }
}
