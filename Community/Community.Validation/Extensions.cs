using Community.CQRS.Abstractions;
using Community.Validation.Decorator;
using Microsoft.Extensions.DependencyInjection;

namespace Community.Validation;

public static class Extensions
{
    public static IServiceCollection AddCqrsValidationDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidateCommandHandlerDecorator<>));
        return services;
    }
}