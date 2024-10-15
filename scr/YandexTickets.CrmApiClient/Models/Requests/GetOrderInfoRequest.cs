using YandexTickets.Common.Models.Requests;
using YandexTickets.Common.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Requests;

/// <summary>
/// Запрос для получения деталей заказа.
/// </summary>
public class GetOrderInfoRequest : RequestBaseWithCity
{
	/// <summary>
	/// Конструктор запроса для получения деталей заказа.
	/// </summary>
	/// <param name="auth">Идентификатор внешней системы.</param>
	/// <param name="cityId">Идентификатор города.</param>
	/// <param name="orderId">Один или несколько идентификаторов заказов.</param>
	public GetOrderInfoRequest(string auth, string cityId, params int[] orderId)
		: base(auth, cityId)
	{
		OrderId = orderId;
	}

	protected override string Action => "crm.order.info";

	/// <summary>
	/// Идентификаторы заказов.
	/// </summary>
	[QueryParameter("order_id")]
	public int[] OrderId { get; set; }
}
