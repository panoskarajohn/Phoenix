namespace Community.Rabbit.Abstractions.Conventions;


public interface IConventions
{
    Type Type { get; }
    string RoutingKey { get; }
    string Exchange { get; }
    string Queue { get; }
}