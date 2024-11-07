using YandexTickets.ApiClients.Agent;
using YandexTickets.ApiClients.Agent.Models.Requests;
using YandexTickets.ApiClients.IntegrationTests.Common;
using YandexTickets.ApiClients.IntegrationTests.Fixtures;
using YandexTickets.ApiClients.IntegrationTests.Models;

namespace YandexTickets.ApiClients.IntegrationTests.Tests;

/// <summary>
/// Интеграционные тесты для методов YandexTicketAgentApiClient.
/// </summary>
public class AgentClientIntegrationTests
	: BaseIntegrationTests<IYandexTicketsAgentApiClient, AgentTestData, AgentFixture>
{
	public AgentClientIntegrationTests(AgentFixture fixture) : base(fixture) { }

	[Fact(DisplayName = "Получение списка городов")]
	public async Task GetCityListAsync()
	{
		var request = new GetCityListRequest();
		var response = await Client.GetCityListAsync(request);

		AssertResponseSuccess(response);
	}
}
