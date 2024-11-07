using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        services.AddHttpClient<IYandexTicketsCrmApiClient, YandexTicketsCrmApiClient>(
            (client, provider) =>
            {
                var config = provider
                    .GetRequiredService<IOptions<CrmTestData>>().Value;
                return new YandexTicketsCrmApiClient(config.Login!, config.Password!, client);
            });

        ServiceProvider = services.BuildServiceProvider();
    }
}