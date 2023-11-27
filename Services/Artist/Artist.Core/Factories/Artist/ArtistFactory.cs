using Artist.Core.Domain;
using Artist.Core.DomainEvent;
using Artist.Core.Enum;
using Artist.Core.ValueObjects;

namespace Artist.Core.Factories.Artist;

public class ArtistFactory : IArtistFactory
{
    /// <summary>
    /// Creates new artist
    /// </summary>
    /// <remarks>
    /// Raises domain event <see cref="NewArtistCreated"/>
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="personalInformation"></param>
    /// <param name="artistDescription"></param>
    /// <returns></returns>
    public ArtistAggregate CreateNew(long id, PersonalInformation personalInformation, string artistDescription)
    {
        var newArtistCreated = new ArtistAggregate(id, personalInformation, artistDescription, ArtistStatus.Draft)
            .WithDates(DateTime.UtcNow, DateTime.UtcNow); 
        newArtistCreated.AddDomainEvent(new NewArtistCreated(id, personalInformation));
        return newArtistCreated;
    }
    
    /// <summary>
    /// Used to map from the database to the domain
    /// </summary>
    /// <remarks>
    /// Does not a raise domain event
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="personalInformation"></param>
    /// <param name="statistics"></param>
    /// <param name="status"></param>
    /// <param name="artistDescription"></param>
    /// <param name="createdAt"></param>
    /// <param name="lastModified"></param>
    /// <returns></returns>
    public ArtistAggregate FromExisting(long id, 
        PersonalInformation personalInformation, 
        ArtistStatistics statistics, 
        ArtistStatus status, 
        string artistDescription, 
        DateTime createdAt, 
        DateTime lastModified)
    {
        return new ArtistAggregate(id, personalInformation, artistDescription, status)
            .WithStatistics(statistics)
            .WithDates(createdAt, lastModified);
    }
}