using Community.Observability;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

services.AddObservability(configuration).AddControllers();
host.UseObservability(configuration);

var app = builder.Build();

app.UseObservability();
app.UseCustomEndpointRouteObservabilty();
app.MapControllers();

app.Run();
