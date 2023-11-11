using Community.Attributes;
using Community.CQRS.Abstractions;

namespace Track.Contracts.Commands;

public class CreateTrackCommand : ICommand
{
    [Hidden]
    public long Id { get; init; }

    public string Title { get; init; }

    public string ArtistId { get; init; }

    public string AlbumId { get; init; }
}