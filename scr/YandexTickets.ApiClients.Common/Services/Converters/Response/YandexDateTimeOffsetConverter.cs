using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YandexTickets.ApiClients.Common.Services.Converters.Response;

/// <summary>
/// Кастомный конвертер для сериализации DateTimeOffset в формате "yyyy-MM-dd HH:mm:sszzz".
/// </summary>
public class YandexDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private readonly string _dateFormat = "yyyy-MM-dd HH:mm:sszzz";
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        var dateString = reader.GetString();

        if (string.IsNullOrWhiteSpace(dateString))
            throw new JsonException("Не удалось десериализовать значение в DateTimeOffset. Входная строка была пустой.");

        return DateTimeOffset.ParseExact(dateString, _dateFormat, _culture);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormat, _culture));
    }
}
