using Artist.Application.Validation;
using Artist.Contracts;
using Community.CQRS;
using Community.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Artist.Application;

public static class Extensions
{
    public static IServiceCollection AddArtistApplication(this IServiceCollection services)
    {
        services
            .AddCqrs()
            .AddCqrsValidationDecorators();
        services.AddScoped<IValidator<CreateArtistCommand>, CreateArtistCommandValidator>();
        return services;
    }
}