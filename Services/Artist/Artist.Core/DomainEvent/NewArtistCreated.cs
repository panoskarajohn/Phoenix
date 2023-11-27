using Artist.Core.Base;
using Artist.Core.ValueObjects;
using Community.CQRS.Abstractions;

namespace Artist.Core.DomainEvent;

public record NewArtistCreated(long Id, PersonalInformation artistPersonalInformation) : IEvent;
