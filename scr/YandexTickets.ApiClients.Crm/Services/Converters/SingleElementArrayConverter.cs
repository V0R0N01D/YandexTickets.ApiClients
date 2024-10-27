using System.Text.Json.Serialization;
using System.Text.Json;

namespace YandexTickets.ApiClients.Crm.Services.Converters;

public class SingleElementArrayConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
		=> typeToConvert.IsGenericType
		&& typeToConvert.GetGenericTypeDefinition() == typeof(List<>);


	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		Type elementType = typeToConvert.GetGenericArguments()[0];

		JsonConverter converter = (JsonConverter)Activator.CreateInstance(
			typeof(SingleElementArrayConverter<>)
			.MakeGenericType(elementType))!;

		return converter;
	}
}


public class SingleElementArrayConverter<T> : JsonConverter<List<T>>
{
	public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var list = new List<T>();

		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException("Ожидался старт массива.");

		// Читаем внешний массив
		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndArray)
				break;

			if (reader.TokenType == JsonTokenType.StartArray)
			{
				// Читаем внутренний массив
				reader.Read();

				// Десериализуем единственный элемент внутреннего массива
				T item = JsonSerializer.Deserialize<T>(ref reader, options)!;
				list.Add(item);

				reader.Read();
				if (reader.TokenType != JsonTokenType.EndArray)
					throw new JsonException("Ожидался конец внутреннего массива.");
			}
			else
			{
				throw new JsonException("Ожидался старт внутреннего массива.");
			}
		}

		return list;
	}

	public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
	{
		// Сериализуем как массив массивов
		writer.WriteStartArray();
		foreach (var item in value)
		{
			JsonSerializer.Serialize(writer, item, options);
		}
		writer.WriteEndArray();
	}
}
