namespace Community.Rabbit.Abstractions.Message;

public interface IMessagePropertiesAccessor
{
    IMessageProperties MessageProperties { get; set; }
}