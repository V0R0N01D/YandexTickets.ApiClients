using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Common.Models.Responses;
using YandexTickets.ApiClients.Common.Services;

namespace YandexTickets.ApiClients.IntegrationTests.Common;

/// <summary>
/// Базовый класс для интеграционных тестов.
/// </summary>
/// <typeparam name="TClient">Тип клиента API, используемого в тестах.</typeparam>
/// <typeparam name="TTestData">Тип данных тестирования, 
/// содержащих необходимые параметры и конфигурацию.</typeparam>
/// <typeparam name="TFixture">Тип фикстуры, 
/// предоставляющей зависимости и настройки для тестов.</typeparam>
public abstract class BaseIntegrationTests<TClient, TTestData, TFixture> : IClassFixture<TFixture>
    where TClient : class
    where TTestData : BaseTestData
    where TFixture : BaseFixture
{
    protected readonly TClient Client;
    protected readonly TTestData TestData;
    protected readonly IServiceProvider ServiceProvider;

    public BaseIntegrationTests(TFixture fixture)
    {
        ServiceProvider = fixture.ServiceProvider;
        TestData = ServiceProvider.GetRequiredService<IOptions<TTestData>>().Value;

        if (string.IsNullOrWhiteSpace(TestData.Login)
            || string.IsNullOrWhiteSpace(TestData.Password))
            throw new InvalidOperationException("В настройках не указан логин или пароль.");
        
        Client = ServiceProvider.GetRequiredService<TClient>();
    }

    protected int GetCityId()
    {
        if (!TestData.CityId.HasValue)
            throw new InvalidOperationException("Не указан идентификатор города в настройках тестовых данных.");

        return TestData.CityId.Value;
    }

    protected static void AssertResponseSuccess<T>(ResponseBase<List<T>> response)
    {
        Assert.True(response.Status == ResponseStatus.Success, response.Error);
        Assert.NotNull(response.Result);
        Assert.NotEmpty(response.Result);
    }
}