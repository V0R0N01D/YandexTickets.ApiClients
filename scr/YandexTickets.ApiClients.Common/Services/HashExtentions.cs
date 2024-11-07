using System.Security.Cryptography;
using System.Text;

namespace YandexTickets.ApiClients.Common.Services;

internal static class HashExtentions
{
	internal static string GetMd5Hash(this string input)
	{
		var inputBytes = Encoding.UTF8.GetBytes(input);
		var hashBytes = MD5.HashData(inputBytes);
		return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
	}

	internal static string GetSha1Hash(this string input)
	{
		var inputBytes = Encoding.UTF8.GetBytes(input);
		var hashBytes = SHA1.HashData(inputBytes);
		return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
	}
}