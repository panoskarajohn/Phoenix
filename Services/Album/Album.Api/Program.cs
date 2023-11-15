using Community.IdGenerator;
using Community.Observability;
using Community.Swagger;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

services
    .AddIdGenerator()
    .AddObservability(configuration)
    .AddSwaggerDocs(configuration)
    .AddControllers();

host.UseObservability(configuration);

var app = builder.Build();

app.UseSwaggerDocs();
app.UseObservability();
app.UseCustomEndpointRouteObservabilty();
app.MapControllers();

app.Run();