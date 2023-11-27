namespace Artist.Core.Base;

public interface IEntity<out TId>
{
    TId Id { get; }
    DateTime LastModified { get; }
    DateTime CreatedAt { get; }
}