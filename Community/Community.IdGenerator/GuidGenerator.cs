namespace Community.IdGenerator;

internal class GuidGenerator : IIdGenerator<Guid>
{
    public Guid New()
    {
        return Guid.NewGuid();
    }
}