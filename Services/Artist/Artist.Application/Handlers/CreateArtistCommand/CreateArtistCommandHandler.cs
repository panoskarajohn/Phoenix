using Artist.Core.Domain;
using Artist.Core.Factories.Artist;
using Artist.Core.Repository;
using Artist.Core.ValueObjects;
using Community.CQRS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Artist.Application.Handlers.CreateArtistCommand;

public class CreateArtistCommandHandler : ICommandHandler<Contracts.Commands.CreateArtistCommand>
{
    private readonly ILogger<CreateArtistCommandHandler> _logger;
    private readonly IArtistFactory _artistFactory;
    private readonly IArtistRepository _artistRepository;
    public CreateArtistCommandHandler(ILogger<CreateArtistCommandHandler> logger, IArtistRepository artistRepository, IArtistFactory artistFactory)
    {
        _logger = logger;
        _artistRepository = artistRepository;
        _artistFactory = artistFactory;
    }
    
    public async Task HandleAsync(Contracts.Commands.CreateArtistCommand command, CancellationToken cancellationToken = default)
    {
        var personalInformation = PersonalInformation.Create(command.Name, 
            command.ArtistNickname, 
            command.LastName, 
            command.BirthDate);
        
        var artistAggregate = _artistFactory.CreateNew(command.Id, personalInformation, command.ArtistDescription);

        await _artistRepository.Add(artistAggregate).ConfigureAwait(false);
    }
}