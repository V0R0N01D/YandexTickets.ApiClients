using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YandexTickets.ApiClients.Crm;
using YandexTickets.ApiClients.IntegrationTests.Common;
using YandexTickets.ApiClients.IntegrationTests.Models;

namespace YandexTickets.ApiClients.IntegrationTests.Fixtures;

public class CrmFixture : BaseFixture
{
    public CrmFixture()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("crm_settings.json").Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.Configure<CrmTestData>(configuration.GetSection("CrmTestData"));

        services.AddHttpClient<IYandexTicketsCrmApiClient, YandexTicketsCrmApiClient>();

        ServiceProvider = services.BuildServiceProvider();
    }
}
