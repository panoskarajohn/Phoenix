namespace Community.Rabbit.Abstractions;

public interface IRabbitMqSerializer
{
    ReadOnlySpan<byte> Serialize(object value);
    object Deserialize(ReadOnlySpan<byte> value, Type type);
    object Deserialize(ReadOnlySpan<byte> value);
}