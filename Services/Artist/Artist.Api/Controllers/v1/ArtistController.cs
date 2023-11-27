using Artist.Contracts.Commands;
using Community.CQRS.Abstractions;
using Community.IdGenerator;
using Microsoft.AspNetCore.Mvc;

namespace Artist.Api.Controllers.v1;

[Route("api/v1/artist")]
public class ArtistController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IIdGenerator<long> _idGenerator;
    
    public ArtistController(ICommandDispatcher commandDispatcher, IIdGenerator<long> idGenerator)
    {
        _commandDispatcher = commandDispatcher;
        _idGenerator = idGenerator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateArtist([FromBody] CreateArtistCommand command)
    {
        command = command with { Id = _idGenerator.Generate()};
        await _commandDispatcher.SendAsync(command);
        return Ok(command.Id);
    }
}