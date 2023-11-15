namespace Community.IdGenerator;

internal class GuidGenerator : IIdGenerator<Guid>
{
    public Guid Generate()
    {
        return Guid.NewGuid();
    }
}