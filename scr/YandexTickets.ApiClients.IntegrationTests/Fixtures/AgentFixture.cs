using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YandexTickets.ApiClients.Agent;
using YandexTickets.ApiClients.IntegrationTests.Common;
using YandexTickets.ApiClients.IntegrationTests.Models;
using YandexTicketsAgentClient;

namespace YandexTickets.ApiClients.IntegrationTests.Fixtures;

public class AgentFixture : BaseFixture
{
	public AgentFixture()
	{
		var configuration = new ConfigurationBuilder().AddJsonFile("agent_settings.json").Build();

		services.AddSingleton<IConfiguration>(configuration);
		services.Configure<AgentTestData>(configuration.GetSection("AgentTestData"));

		services.AddHttpClient<IYandexTicketsAgentApiClient, YandexTicketAgentApiClient>();

		ServiceProvider = services.BuildServiceProvider();
	}
}
