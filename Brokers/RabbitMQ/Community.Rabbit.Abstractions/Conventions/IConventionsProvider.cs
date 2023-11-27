namespace Community.Rabbit.Abstractions.Conventions;

public interface IConventionsProvider
{
    IConventions Get<T>();
    IConventions Get(Type type);
}