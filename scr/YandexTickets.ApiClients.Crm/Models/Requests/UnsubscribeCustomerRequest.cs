using YandexTickets.ApiClients.Common.Models.Requests;
using YandexTickets.ApiClients.Common.Services.Attributes;

namespace YandexTickets.ApiClients.Crm.Models.Requests;

/// <summary>
/// Запрос для отписки покупателя от рассылок в системе Яндекс Билеты.
/// </summary>
public class UnsubscribeCustomerRequest : RequestBaseWithCity
{
    /// <summary>
    /// Конструктор запроса для отписки покупателя от рассылок.
    /// </summary>
    /// <param name="cityId">Идентификатор города.</param>
    /// <param name="email">Электронная почта покупателя для отписки.</param>
    public UnsubscribeCustomerRequest(int cityId, string email)
        : base(cityId)
    {
        Email = email;
    }

    /// <inheritdoc />
    protected override string Action => "crm.customer.unsubscribe";

    /// <summary>
    /// Электронная почта покупателя для отписки.
    /// </summary>
    [QueryParameter("email")]
    public string Email { get; set; }
}