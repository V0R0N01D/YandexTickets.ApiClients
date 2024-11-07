using System.Security.Cryptography;
using System.Text;

namespace YandexTickets.ApiClients.Common.Services;

/// <summary>
/// Класс для управления ключем для авторизации.
/// </summary>
internal class AuthService
{
    /// <summary>
    /// Время в течении которого ключ авторизации является активным.
    /// </summary>
    private static readonly TimeSpan LifeTime = TimeSpan.FromHours(6);

    /// <summary>
    /// Время когда ключ для авторизации перестанет быть актуальным.
    /// </summary>
    private DateTime? _expires;

    /// <summary>
    /// Актуальный ключ для авторизации.
    /// </summary>
    private string _auth = null!;

    private readonly string _login;
    private readonly string _password;

    /// <summary>
    /// Инициализирует новый экземпляр с указанными данными.
    /// </summary>
    /// <param name="login">Логин для создания ключа авторизации.</param>
    /// <param name="password">Пароль для создания ключа авторизации.</param>
    /// <exception cref="ArgumentException"></exception>
    internal AuthService(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Логин не может быть пустым.");
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Пароль не может быть пустым.");

        _login = login;
        _password = password;

        GenerateAuthToken();
    }

    /// <summary>
    /// Получение ключа авторизации для запроса.
    /// </summary>
    /// <returns>Ключ авторизации.</returns>
    internal string GetAuthToken()
    {
        if (DateTime.Now > _expires)
            GenerateAuthToken();

        return _auth;
    }


    /// <summary>
    /// Генерирует ключ авторизации на основе логина и пароля.
    /// Устанавливает в поле <see cref="_auth"/>.
    /// Формат: "login:sha1(md5(password) + timestamp):timestamp"
    /// </summary>
    private void GenerateAuthToken()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var passwordMd5 = _password.GetMd5Hash();
        var hashInput = passwordMd5 + timestamp;
        var passwordSha1 = hashInput.GetSha1Hash();

        _auth = $"{_login}:{passwordSha1}:{timestamp}";
        _expires = DateTime.Now.Add(LifeTime);
    }
}