using Community.Attributes;
using Community.CQRS.Abstractions;

namespace Artist.Contracts;

public record CreateArtistCommand : ICommand
{
    [Hidden]
    public long Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public string ArtistNickname { get; init; }
    public string ArtistDescription { get; init; }
}