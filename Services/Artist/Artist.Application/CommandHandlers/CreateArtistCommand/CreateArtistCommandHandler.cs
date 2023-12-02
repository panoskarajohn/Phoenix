using Artist.Core.Factories.Artist;
using Artist.Core.Repository;
using Artist.Core.ValueObjects;
using Community.Context.Abstractions;
using Community.CQRS.Abstractions;
using Community.Rabbit.Abstractions.Publisher;
using Microsoft.Extensions.Logging;

namespace Artist.Application.CommandHandlers.CreateArtistCommand;

public class CreateArtistCommandHandler : ICommandHandler<Contracts.Commands.CreateArtistCommand>
{
    private readonly ILogger<CreateArtistCommandHandler> _logger;
    private readonly IArtistFactory _artistFactory;
    private readonly IArtistRepository _artistRepository;
    private readonly IBusPublisher _busPublisher;
    private readonly IContext _context;
    
    public CreateArtistCommandHandler(ILogger<CreateArtistCommandHandler> logger, IArtistRepository artistRepository, IArtistFactory artistFactory, IBusPublisher busPublisher, IContext context)
    {
        _logger = logger;
        _artistRepository = artistRepository;
        _artistFactory = artistFactory;
        _busPublisher = busPublisher;
        _context = context;
    }
    
    public async Task HandleAsync(Contracts.Commands.CreateArtistCommand command, CancellationToken cancellationToken = default)
    {
        var personalInformation = PersonalInformation.Create(command.Name, 
            command.ArtistNickname, 
            command.LastName, 
            command.BirthDate);
        
        var artistAggregate = _artistFactory.CreateNew(command.Id, personalInformation, _context.UserId, command.ArtistDescription);

        await _artistRepository.Add(artistAggregate).ConfigureAwait(false);
        await _busPublisher.PublishAsync(artistAggregate.DomainEvents.First());
    }
}