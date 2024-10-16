using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;
using YandexTickets.CrmApiClient.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список покупателей.
/// </summary>
[SingleElementArray]
public class CustomerListResponse : ResponseBase<List<Customer>> { }
