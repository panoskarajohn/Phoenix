using Community.Mongo;

namespace Artist.Infrastructure.Documents.Artist;

public class ArtistDocument : IIdentifiable<long>
{
    public required long Id { get; init; }
    public required ArtistPersonalInformation PersonalInformation { get; init; }
    public required string Status { get; init; }
    public required DateTime BirthDate { get; init; }
    public required ArtistStatistics Statistics { get; init; }
    
    public required long Version { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required string CreatedBy { get; init; }
}

public class ArtistStatistics
{
    public required int Followers { get; init; }
    public required int MonthlyListeners { get; init; }
    public required int Views { get; init; }
    public required int Likes { get; init; }
    
    public static ArtistStatistics Default => new()
    {
        Followers = 0,
        Likes = 0,
        MonthlyListeners = 0,
        Views = 0
    };
}

public class ArtistPersonalInformation
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string NickName { get; init; }
    public required DateTime BirthDate { get; init; }
}