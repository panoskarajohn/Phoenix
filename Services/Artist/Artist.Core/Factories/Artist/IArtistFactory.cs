using Artist.Core.Domain;
using Artist.Core.Enum;
using Artist.Core.ValueObjects;

namespace Artist.Core.Factories.Artist;

public interface IArtistFactory
{
    public ArtistAggregate CreateNew(long id, PersonalInformation personalInformation, string createdBy, string artistDescription);

    public ArtistAggregate FromExisting(long id,
        PersonalInformation personalInformation,
        ArtistStatistics statistics,
        ArtistStatus status,
        AuditInformation auditInformation,
        string artistDescription);
}