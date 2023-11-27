namespace Community.Rabbit.Abstractions;

public interface IExceptionToMessageMapper
{
    object Map(Exception exception, object message);
}