using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YandexTickets.Common.Services.Converters.Response;

/// <summary>
/// Кастомный конвертер для сериализации DateTime в формате "yyyy-MM-dd HH:mm:ss".
/// </summary>
public class YandexDateTimeConverter : JsonConverter<DateTime>
{
    const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (string.IsNullOrWhiteSpace(dateString))
            throw new JsonException("Не удалось распарсить значение в DateTime. Входная строка была пустой.");

        return DateTime.ParseExact(dateString, DateTimeFormat, _culture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateTimeFormat, _culture));
    }
}
