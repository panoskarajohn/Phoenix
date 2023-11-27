using Ardalis.GuardClauses;

namespace Artist.Core.ValueObjects;

public record ArtistStatistics
{
    public int MonthlyListeners { get; }
    public int Followers { get; }
    public int Views { get; }
    public int Likes { get; }

    private ArtistStatistics(int monthlyListeners, int followers, int views, int likes)
    {
        MonthlyListeners = monthlyListeners;
        Followers = followers;
        Views = views;
        Likes = likes;
    }

    public static ArtistStatistics Create(int monthlyListeners, int followers, int views, int likes)
    {
        Guard.Against.Negative(monthlyListeners);
        Guard.Against.Negative(followers);
        Guard.Against.Negative(views);
        Guard.Against.Negative(likes);
        return new(monthlyListeners, followers, views, likes);
    }
}