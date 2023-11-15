using Community.CQRS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Artist.Application.Handlers.CreateArtistCommand;

public class CreateArtistCommandHandler : ICommandHandler<Contracts.CreateArtistCommand>
{
    private readonly ILogger<CreateArtistCommandHandler> _logger;
    public CreateArtistCommandHandler(ILogger<CreateArtistCommandHandler> logger)
    {
        _logger = logger;
    }
    public Task HandleAsync(Contracts.CreateArtistCommand command, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("I was called");
        return Task.CompletedTask;
    }
}