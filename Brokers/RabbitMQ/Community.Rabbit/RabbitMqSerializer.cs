using System.Text.Json;
using System.Text.Json.Serialization;
using Community.Rabbit.Abstractions;

namespace Community.Rabbit;

public class RabbitMqSerializer : IRabbitMqSerializer
{
    private readonly JsonSerializerOptions _options;

    public RabbitMqSerializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
        };
    }

    public ReadOnlySpan<byte> Serialize(object value) => JsonSerializer.SerializeToUtf8Bytes(value, _options);

    public object Deserialize(ReadOnlySpan<byte> value, Type type) => JsonSerializer.Deserialize(value, type, _options);

    public object Deserialize(ReadOnlySpan<byte> value) => JsonSerializer.Deserialize(value, typeof(object), _options);
}