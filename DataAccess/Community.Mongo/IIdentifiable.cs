namespace Community.Mongo;

public interface IIdentifiable<out T>
{
    T Id { get; }
}