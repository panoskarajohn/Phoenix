using Artist.Core.ValueObjects;
using Community.CQRS.Abstractions;

namespace Artist.Core.DomainEvent;

public record StatisticsUpdated(long Id, ArtistStatistics Statistics) : IEvent;
