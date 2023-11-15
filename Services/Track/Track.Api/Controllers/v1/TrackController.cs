using Asp.Versioning;
using Community.Context.Abstractions;
using Community.IdGenerator;
using Microsoft.AspNetCore.Mvc;

namespace Track.Api.Controllers;

[ApiController]
[Route("api/v1/track")]
public class TrackController : ControllerBase
{
    private readonly ILogger<TrackController> _logger;
    private readonly IIdGenerator<long> _idGenerator;
    
    public TrackController(IContext context, ILogger<TrackController> logger, IIdGenerator<long> idGenerator)
    {
        _logger = logger;
        _idGenerator = idGenerator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}