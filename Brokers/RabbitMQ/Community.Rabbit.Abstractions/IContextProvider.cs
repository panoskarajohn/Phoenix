namespace Community.Rabbit.Abstractions;

public interface IContextProvider
{
    string HeaderName { get; }
    object Get(IDictionary<string, object> headers);
}