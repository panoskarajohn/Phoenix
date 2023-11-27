namespace Community.Rabbit.Abstractions.Correlation;

public interface ICorrelationContextAccessor
{
    object CorrelationContext { get; set; }
}