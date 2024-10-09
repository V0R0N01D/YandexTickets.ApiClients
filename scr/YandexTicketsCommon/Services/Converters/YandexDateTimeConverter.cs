using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YandexTicketsCommon.Services.Converters;

/// <summary>
/// Кастомный конвертер для сериализации DateTime в формате "yyyy-MM-dd HH:mm:ss".
/// </summary>
public class YandexDateTimeConverter : JsonConverter<DateTime>
{
	const string Format = "yyyy-MM-dd HH:mm:ss";
	private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		var dateString = reader.GetString();
		return DateTime.ParseExact(dateString, Format, _culture);
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString(Format, _culture));
	}
}
