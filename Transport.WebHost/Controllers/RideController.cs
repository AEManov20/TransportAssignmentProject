using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;

namespace Transport.WebHost.Controllers;

[ApiController]
[Route("api/ride")]
[Authorize]
public class RideController : Controller
{
    private readonly IRideService rideService;

    public RideController(IRideService rideService)
    {
        this.rideService = rideService;
    }

    [HttpGet("discover")]
    public async Task<ActionResult<ICollection<RideViewModel>>> DiscoverRidesAsync()
    {
        return Ok(await rideService.GetRequestedRidesAsync());
    }

    [HttpGet("google-maps-link")]
    public async Task<ActionResult<string>> GetGoogleMapsLinkAsync([FromQuery] Guid rideId)
    {
        var link = await rideService.GetGoogleMapsLinkAsync(rideId);

        if (link == null)
            return NotFound("ride-not-found");
        
        return Ok(link);
    }
}
