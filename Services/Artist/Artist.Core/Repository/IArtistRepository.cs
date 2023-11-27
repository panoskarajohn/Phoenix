using Artist.Core.Domain;

namespace Artist.Core.Repository;

public interface IArtistRepository
{
    public Task Add(ArtistAggregate artistAggregate);
}