using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Community.IdGenerator;

public static class Extensions
{
    public static IServiceCollection AddIdGenerator(this IServiceCollection services, int generatorId = 1)
    {
        services.AddSingleton<IIdGenerator<long>>(new SnowflakeGenerator(generatorId));
        services.AddSingleton<IIdGenerator<Guid>>(new GuidGenerator());
        return services;
    }
}