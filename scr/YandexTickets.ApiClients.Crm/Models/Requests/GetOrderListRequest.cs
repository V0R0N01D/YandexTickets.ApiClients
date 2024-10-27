using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;
using YandexTickets.ApiClients.Common.Services.Converters.Request;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

/// <summary>
/// Класса запроса для получения списка заказов.
/// </summary>
public class GetOrderListRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор класса запроса списка заказов.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="orderId">Идентификатор заказа.</param>
	/// <param name="startDate">Дата операции начиная с которой (включительно) заказы возвращаются в ответе.</param>
	/// <param name="endDate">Дата операции до которой (включительно) заказы возвращаются в ответе.</param>
	/// <param name="status">Статус заказа.</param>
	public GetOrderListRequest(string auth, int cityId, int? orderId = null,
		DateOnly? startDate = null, DateOnly? endDate = null, OrderStatus? status = null)
		: base(auth, cityId)
	{
		OrderId = orderId;
		StartDate = startDate;
		EndDate = endDate;
		Status = status;
	}

	protected override string Action => "crm.order.list";

	/// <summary>
	/// Идентификатор заказа.
	/// </summary>
	[QueryParameter("order_id", false)]
	public int? OrderId { get; set; }

	/// <summary>
	/// Дата операции начиная с которой (включительно) заказы возвращаются в ответе.
	/// </summary>
	[QueryParameter("start_date", false)]
	[QueryParameterConverter(typeof(DateOnlyConverter))]
	public DateOnly? StartDate { get; set; }

	/// <summary>
	/// Дата операции до которой (включительно) заказы возвращаются в ответе.
	/// </summary>
	[QueryParameter("end_date", false)]
	[QueryParameterConverter(typeof(DateOnlyConverter))]
	public DateOnly? EndDate { get; set; }

	/// <summary>
	/// Статус заказа.
	/// </summary>
	[QueryParameter("status", false)]
	[QueryParameterConverter(typeof(EnumConverter))]
	public OrderStatus? Status { get; set; }
}