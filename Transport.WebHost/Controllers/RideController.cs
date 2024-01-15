using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;

namespace Transport.WebHost.Controllers;

[ApiController]
[Route("api/ride")]
[Authorize]
class RideController : Controller
{
    IRideService rideService;

    public RideController(IRideService rideService)
    {
        this.rideService = rideService;
    }

    [HttpGet("discover")]
    public async Task<ActionResult<ICollection<RideViewModel>>> DiscoverRidesAsync()
    {
        return Ok(await rideService.GetRequestedRidesAsync());
    }
}
