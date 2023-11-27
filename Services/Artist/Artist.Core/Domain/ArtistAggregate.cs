using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Artist.Core.Base;
using Artist.Core.DomainEvent;
using Artist.Core.Enum;
using Artist.Core.ValueObjects;

namespace Artist.Core.Domain;

public class ArtistAggregate : BaseAggregateRoot<long>
{
    public PersonalInformation PersonalInformation { get; private set; }
    public ArtistStatus ArtistStatus { get; private set; }
    public ArtistStatistics? ArtistStatistics { get; private set; }
    
    public string Description { get; }
    
    internal ArtistAggregate(long id, 
        PersonalInformation personalInformation,
        string description, 
        ArtistStatus status)
    {
        Id = id;
        PersonalInformation = personalInformation;
        Description = description;
        ArtistStatus = status;
    }
    
    public ArtistAggregate WithStatistics(ArtistStatistics artistStatistics)
    {
        ArtistStatistics = artistStatistics;
        return this;
    }
    
    public ArtistAggregate WithDates(DateTime createdAt, DateTime lastModified)
    {
        CreatedAt = createdAt;
        LastModified = lastModified;
        return this;
    }

    public void UpdateStatistics(ArtistStatistics artistStatistics)
    {
        ArtistStatistics = artistStatistics;
        AddDomainEvent(new StatisticsUpdated(Id, ArtistStatistics));
    }
}