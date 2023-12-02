namespace Artist.Core.Base;

public abstract class Entity<TId> : IEntity<TId>
{
    protected Entity(TId id) => Id = id;

    protected Entity()
    {
    }

    public long Version { get; set; }
    public TId Id { get; protected set; }
}