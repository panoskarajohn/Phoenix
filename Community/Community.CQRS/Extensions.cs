using Community.Attributes;
using Community.CQRS.Abstractions;
using Community.CQRS.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Community.CQRS;

public static class Extensions
{
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                .WithoutAttribute(typeof(Decorator)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                    .WithoutAttribute(typeof(Decorator)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
    
    private static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                    .WithoutAttribute(typeof(Decorator)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }


    public static IServiceCollection AddCqrs(this IServiceCollection services) => services
        .AddCommands()
        .AddEvents()
        .AddQueries();


    public static IServiceCollection AddLoggingDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        return services;
    }
}