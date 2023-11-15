using Artist.Application;
using Community.CQRS;
using Community.IdGenerator;
using Community.Observability;
using Community.Swagger;
using Community.Validation;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

services
    .AddIdGenerator()
    .AddObservability(configuration)
    .AddSwaggerDocs(configuration)
    .AddControllers();

//Layers
services.AddArtistApplication();

host.UseObservability(configuration);

var app = builder.Build();

app.UseSwaggerDocs();
app.UseObservability();
app.UseCustomEndpointRouteObservabilty();
app.MapControllers();

app.Run();