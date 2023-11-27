using Artist.Core.Base;
using Artist.Core.ValueObjects;
using Community.Attributes;
using Community.CQRS.Abstractions;

namespace Artist.Core.DomainEvent;

[RabbitMessage("artist_api_output", "artist_created")]
public record NewArtistCreated(long Id, PersonalInformation artistPersonalInformation) : IEvent;
