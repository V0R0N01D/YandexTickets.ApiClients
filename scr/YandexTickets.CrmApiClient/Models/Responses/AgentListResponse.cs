﻿using YandexTickets.Common.Models.Responses;
using YandexTickets.CrmApiClient.Models.DTO;
using YandexTickets.CrmApiClient.Services.Attributes;

namespace YandexTickets.CrmApiClient.Models.Responses;

/// <summary>
/// Ответ, содержащий список агентов.
/// </summary>
[SingleElementArray]
public class AgentListResponse : ResponseBase<List<Agent>> { }