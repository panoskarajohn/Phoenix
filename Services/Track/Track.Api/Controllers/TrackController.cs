using Community.Context;
using Microsoft.AspNetCore.Mvc;

namespace Track.Api.Controllers;

[Route("api/v1/track")]
public class TrackController : ControllerBase
{
    private readonly IContext _context;
    private readonly ILogger<TrackController> _logger;
    
    public TrackController(IContext context, ILogger<TrackController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation($"TrackController.Get called {_context.CorrelationId} {_context.RequestId} {_context.TraceId} UserId: {_context.UserId} IpAddress: {_context.IpAddress} UserAgent: {_context.UserAgent}");    
        return Ok();
    }
}