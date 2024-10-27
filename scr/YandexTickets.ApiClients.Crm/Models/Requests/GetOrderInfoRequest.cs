using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;
using YandexTickets.ApiClients.Common.Services.Converters.Request;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

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
	/// <param name="ordersId">Один или несколько идентификаторов заказов.</param>
	public GetOrderInfoRequest(string auth, int cityId, params int[] ordersId)
		: base(auth, cityId)
	{
		if (ordersId == null || ordersId.Length == 0)
			throw new ArgumentNullException(nameof(ordersId),
				"Необходимо указать хотя бы один идентификатор заказа.");

		OrdersId = ordersId;
	}

	protected override string Action => "crm.order.info";

	/// <summary>
	/// Идентификаторы заказов.
	/// </summary>
	[QueryParameter("order_id")]
	[QueryParameterConverter(typeof(EnumerableConverter))]
	public int[] OrdersId { get; set; }
}
