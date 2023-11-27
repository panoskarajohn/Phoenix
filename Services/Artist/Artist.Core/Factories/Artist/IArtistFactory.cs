using Artist.Core.Domain;
using Artist.Core.Enum;
using Artist.Core.ValueObjects;

namespace Artist.Core.Factories.Artist;

public interface IArtistFactory
{
    public ArtistAggregate CreateNew(long id, PersonalInformation personalInformation, string artistDescription);

    public ArtistAggregate FromExisting(long id,
        PersonalInformation personalInformation,
        ArtistStatistics statistics,
        ArtistStatus status,
        string artistDescription,
        DateTime createdAt,
        DateTime lastModified);
}