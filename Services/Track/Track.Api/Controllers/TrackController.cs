using Community.Context.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Track.Api.Controllers;

[Route("api/v1/track")]
public class TrackController : ControllerBase
{
    private readonly ILogger<TrackController> _logger;
    
    public TrackController(IContext context, ILogger<TrackController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}