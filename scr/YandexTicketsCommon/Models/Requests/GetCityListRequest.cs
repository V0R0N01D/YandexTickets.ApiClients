﻿namespace YandexTicketsCommon.Models.Requests;

/// <summary>
/// Запрос для получения списка городов.
/// </summary>
public class GetCityListRequest : RequestBase
{
	/// <summary>
	/// Конструктор запроса для получения списка городов.
	/// </summary>
	/// <param name="identifier">Идентификатор внешней системы.</param>
	public GetCityListRequest(string identifier) : base(identifier) { }

	protected override string Action => "crm.city.list";
}
