using Artist.Core.Domain;
using Artist.Infrastructure.Documents.Artist;

namespace Artist.Infrastructure.Mappers.Artist;

public class ArtistToDocument
{
    public static ArtistDocument Map(ArtistAggregate artist)
        => new()
        {
            Id = artist.Id,
            BirthDate = artist.PersonalInformation.BirthDate,
            PersonalInformation = new()
            {
                BirthDate = artist.PersonalInformation.BirthDate,
                LastName = artist.PersonalInformation.LastName,
                Name = artist.PersonalInformation.Name,
                NickName = artist.PersonalInformation.NickName
            },
            Statistics = Map(artist.ArtistStatistics),
            Status = artist.ArtistStatus.ToString(),
            CreatedAt = artist.AuditInformation.CreatedAt,
            UpdatedAt = artist.AuditInformation.LastModified,
            Version = artist.Version,
            CreatedBy = artist.AuditInformation.ModifiedBy
        };
    
    private static ArtistStatistics Map(Core.ValueObjects.ArtistStatistics? artistStatistics)
        => artistStatistics is null ? ArtistStatistics.Default : new() 
        {
            Followers = artistStatistics.Followers,
            Likes = artistStatistics.Likes,
            MonthlyListeners = artistStatistics.MonthlyListeners,
            Views = artistStatistics.Views
        };
}