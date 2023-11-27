using Artist.Core.Repository;
using Artist.Infrastructure.Documents.Artist;
using Artist.Infrastructure.Repositories;
using Community.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Artist.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddArtistInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongo(configuration);
        
        services.AddMongoRepository<ArtistDocument, long>("artists");
        services.AddScoped<IArtistRepository, ArtistRepository>();
        
        return services;
    }
}