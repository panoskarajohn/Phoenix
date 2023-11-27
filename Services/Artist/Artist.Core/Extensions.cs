using Artist.Core.Factories.Artist;
using Microsoft.Extensions.DependencyInjection;

namespace Artist.Core;

public static class Extensions
{
    public static IServiceCollection AddArtistCore(this IServiceCollection services)
    {
        services.AddSingleton<IArtistFactory, ArtistFactory>();
        return services;
    }
}