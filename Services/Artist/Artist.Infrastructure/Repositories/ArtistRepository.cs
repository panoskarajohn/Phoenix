using Artist.Core.Domain;
using Artist.Core.Repository;
using Artist.Infrastructure.Documents.Artist;
using Artist.Infrastructure.Mappers.Artist;
using Community.Context.Abstractions;
using Community.Mongo.Repository;

namespace Artist.Infrastructure.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly IMongoRepository<ArtistDocument, long> _artistRepository;

    public ArtistRepository(IMongoRepository<ArtistDocument, long> artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public Task Add(ArtistAggregate artistAggregate)
    {
        var artistDocument = ArtistToDocument.Map(artistAggregate);
        return _artistRepository.AddAsync(artistDocument);
    }
}