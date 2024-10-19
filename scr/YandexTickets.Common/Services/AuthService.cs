using System.Security.Cryptography;
using System.Text;

namespace YandexTickets.Common.Services;

public static class AuthService
{
	/// <summary>
	/// Генерирует ключ авторизации на основе логина и пароля.
	/// Формат: "login:sha1(md5(password) + timestamp):timestamp"
	/// </summary>
	/// <param name="login">Логин пользователя.</param>
	/// <param name="password">Пароль пользователя.</param>
	/// <returns>Строка сгенерированного ключа авторизации.</returns>
	/// <exception cref="ArgumentException"></exception>
	public static string GenerateAuthToken(string login, string password)
	{
		if (string.IsNullOrWhiteSpace(login))
			throw new ArgumentException("Логин не может быть пустым.", nameof(login));
		if (string.IsNullOrWhiteSpace(password))
			throw new ArgumentException("Пароль не может быть пустым.", nameof(password));

		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
		var passwordMd5 = GetMd5Hash(password);
		var hashInput = passwordMd5 + timestamp;
		var passwordSha1 = GetSha1Hash(hashInput);
		var authToken = $"{login}:{passwordSha1}:{timestamp}";
		return authToken;
	}

	private static string GetMd5Hash(string input)
	{
		var inputBytes = Encoding.UTF8.GetBytes(input);
		var hashBytes = MD5.HashData(inputBytes);
		return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
	}

	private static string GetSha1Hash(string input)
	{
		var inputBytes = Encoding.UTF8.GetBytes(input);
		var hashBytes = SHA1.HashData(inputBytes);
		return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
	}
}
