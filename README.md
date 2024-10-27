# YandexTickets.ApiClients

Библиотеки для интеграции с CRM и Agent API Яндекс.Билетов, упрощающие получение информации о мероприятиях и продажу билетов.

## Оглавление
- [Общее описание](#общее-описание)
  - [Официальная документация](#официальная-документация)
- [Авторизация](#авторизация)
- [CRM API Клиент](#crm-api-клиент)
  - [Возможности](#возможности)
  - [Установка](#установка)
  - [Использование](#использование)
  - [Методы клиента](#методы-клиента)
  - [Полезная информация](#полезная-информация)
  - [Тестирование](#тестирование)
- [Agent API Клиент](#agent-api-клиент)
  - [Возможности](#возможности-1)
  - [Установка](#установка-1)
  - [Использование](#использование-1)
  - [Методы API](#методы-api)
  - [Тестирование](#тестирование-1)

## Общее описание

В проекте реализовано 2 отдельных клиента для интеграции с API Яндекс.Билетов:

- **CRM API** - позволяет получать информацию о результатах организации.
- **Agent API** (в разработке) - позволяет управлять доступными билетами, осуществлять продажи и управлять заказами.

### Официальная документация

- [Документация CRM API Яндекс.Билетов](https://yandex.ru/dev/tickets/crm/doc/ru/concepts/)
- [Документация Agent API Яндекс.Билетов](https://yandex.ru/dev/tickets/agent/doc/ru/concepts/)

## Авторизация

Для выполнения запросов к API необходимо сгенерировать токен авторизации согласно [данной документации](https://yandex.ru/dev/tickets/agent/doc/ru/concepts/access):

```csharp
using YandexTickets.ApiClients.Common.Services;

string login = "login";
string password = "password";
string authToken = AuthService.GenerateAuthToken(login, password);
```

## CRM API Клиент

### Возможности:

- Получать списки городов, мероприятий, событий, покупателей, агентов и проданных билетов.
- Получать информацию о заказах.
- Строить отчеты о событиях.
- Отписывать покупателей от рассылок.

### Установка

Пакет доступен через NuGet - [YandexTickets.ApiClients.Crm](https://www.nuget.org/packages/YandexTickets.ApiClients.Crm/)

### Использование

#### Инициализация клиента

Для начала работы необходимо создать экземпляр `YandexTicketsCrmApiClient`, предоставив ему экземпляр `HttpClient`:

```csharp
using YandexTickets.ApiClients.Crm;

var httpClient = new HttpClient();
var crmClient = new YandexTicketsCrmApiClient(httpClient);
```

#### Выполнение запросов

##### Получение списка городов

```csharp
using YandexTickets.ApiClients.Common.Models.Enums;
using YandexTickets.ApiClients.Crm.Models.Requests;
using YandexTickets.ApiClients.Crm.Models.Responses;

// Создаем запрос
var request = new GetCityListRequest(authToken);

// Отправляем запрос
var response = await crmClient.GetCityListAsync(request);

if (response.Status == ResponseStatus.Success)
{
    var cities = response.Result;
    // Обрабатываем список городов
}
else
{
    Console.WriteLine($"Ошибка: {response.Error}");
}
```

##### Получение списка событий

```csharp
int cityId = "идентификатор_города";
var request = new GetEventListRequest(authToken, cityId);
var response = await crmClient.GetEventListAsync(request);

if (response.Status == ResponseStatus.Success)
{
    var events = response.Result;
    // Обрабатываем список событий
}
else
{
    Console.WriteLine($"Ошибка: {response.Error}");
}
```

### Методы клиента

#### Информационные запросы

- `GetCityListAsync` - возвращает список городов.
- `GetActivityListAsync` - возвращает список мероприятий.
- `GetEventListAsync` - возвращает список событий.
- `GetOrderListAsync` - возвращает список заказов.
- `GetOrderInfoAsync` - возвращает детальную информацию о заказах.
- `GetCustomerListAsync` - возвращает список покупателей.
- `GetAgentListAsync` - возвращает список агентов.
- `GetEventReportAsync` - возвращает отчеты о событиях.
- `GetSoldTicketsAsync` - возвращает список проданных билетов. (Примечание: этот метод не описан в официальной документации API.)

#### Операции с покупателями

- `UnsubscribeCustomerAsync` - отписывает покупателя от рассылок в системе Яндекс.Билеты.

### Полезная информация

- Используйте `AuthService.GenerateAuthToken(string login, string password)` для генерации токена авторизации, необходимого для выполнения запросов к API.
- Классы запросов находятся в пространстве `YandexTickets.ApiClients.Crm.Models.Requests`.
- Ответы от API представлены в виде классов из пространства `YandexTickets.ApiClients.Crm.Models.Responses`.

### Тестирование

Проект включает интеграционные тесты для проверки функциональности CRM клиента.

Перед запуском тестов необходимо создать файл с необходимыми данными:
- Переименуйте файл crm_settings.template.json в crm_settings.json.
- Обновите значения в файле на валидные данные для вашего окружения.


## Agent API Клиент 
Примечание: Клиент для Agent API находится в разработке.

### Возможности:
Будет доступно после завершения разработки.

### Установка
Пакет для Agent API клиента будет доступен через NuGet после завершения разработки.

### Использование
Будет доступно после завершения разработки.

### Методы API
Будет доступно после завершения разработки.

### Тестирование

Проект включает интеграционные тесты для проверки функциональности Agent клиента.

Перед запуском тестов необходимо создать файл с необходимыми данными:
- Переименуйте файл agent_settings.template.json в agent_settings.json.
- Обновите значения в файле на валидные данные для вашего окружения.


## Дальнейшие планы

- Реализация клиента для Agent API (в разработке).
