using Community.Rabbit.Abstractions.Message;

namespace Community.Rabbit.Abstractions;

public interface IExceptionToFailedMessageMapper
{
    FailedMessage Map(Exception exception, object message);
}