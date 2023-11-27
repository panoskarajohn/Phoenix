using Artist.Application;
using Artist.Core;
using Artist.Infrastructure;
using Community.Exception;
using Community.IdGenerator;
using Community.Observability;
using Community.Swagger;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

services
    .AddErrorMiddleware()
    .AddIdGenerator()
    .AddObservability(configuration)
    .AddSwaggerDocs(configuration)
    .AddControllers();

//Layers
services.AddArtistCore();
services.AddArtistApplication();
services.AddArtistInfrastructure(configuration);

host.UseObservability(configuration);

var app = builder.Build();
app.UseErrorMiddleware();
app.UseSwaggerDocs();
app.UseObservability();
app.UseCustomEndpointRouteObservabilty();
app.MapControllers();

app.Run();