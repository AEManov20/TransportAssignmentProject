using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;
using Transport.Data.Models;

namespace Transport.WebHost.Controllers;

[Authorize]
[Route("api/user")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService userService;
    private readonly IAuthedUser authedUser;
    private readonly IRideService rideService;
    private readonly IMapper mapper;

    public UserController(
        IUserService userService,
        IAuthedUser authedUser,
        IRideService rideService,
        IMapper mapper)
    {
        this.userService = userService;
        this.authedUser = authedUser;
        this.rideService = rideService;
        this.mapper = mapper;
    }

    [HttpPut("@me/ping-location")]
    public async Task<IActionResult> PingLocationAsync([FromQuery] LocationModel location)
    {
        if (!await userService.UpdateUserLocationAsync(authedUser.UserId, location))
            return BadRequest("failed-updating");

        return Ok();
    }

    [HttpGet("@me/location")]
    public async Task<ActionResult<LocationModel>> GetLocationAsync()
    {
        var location = await userService.GetUserLocationAsync(authedUser.UserId);

        return Ok(location);
    }

    [HttpGet("@me")]
    public async Task<ActionResult<UserViewModel>> GetSelfUserAsync()
    {
        var user = await userService.GetUserByIdAsync(authedUser.UserId);

        if (user == null)
            return BadRequest("user-not-found");

        return Ok(user);
    }

    [HttpPut("@me")]
    public async Task<ActionResult<UserViewModel>> UpdateSelfUserAsync([FromBody] UserUpdateModel userUpdate)
    {
        var (user, identityResult) = await userService.UpdateUserAsync(authedUser.UserId, userUpdate);
        
        if (user == null || identityResult is not { Succeeded: true })
            return BadRequest("failed-updating");

        return Ok(user);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public async Task<ActionResult<ICollection<RideViewModel>>> GetSelfRidesBaseAsync(bool ongoing)
    {
        var rides = await rideService.GetUserRidesAsync(authedUser.UserId, ongoing);

        if (rides == null)
            return BadRequest("user-not-found");

        return Ok(rides);
    }

    [HttpGet("@me/rides")]
    public async Task<ActionResult<ICollection<RideViewModel>>> GetSelfRidesAsync() =>
        await GetSelfRidesBaseAsync(false);

    [HttpGet("@me/rides/ongoing")]
    public async Task<ActionResult<ICollection<RideViewModel>>> GetSelfRidesOngoingAsync() =>
        await GetSelfRidesBaseAsync(true);

    [HttpPost("@me/rides")]
    public async Task<ActionResult<RideViewModel>> RequestNewRide([FromBody] RideInputModel rideInput)
    {
        var ride = await rideService.CreateRideAsync(rideInput, authedUser.UserId);

        if (ride == null)
            return BadRequest("failed-creating");

        return Ok(ride);
    }

    [HttpPost("@me/rides/cancel")]
    public async Task<ActionResult<RideViewModel>> CancelRide([FromQuery] CancelRideModel cancelRide)
    {
        var ride = await rideService.GetRideByIdAsync(cancelRide.RideId);
        
        if (ride == null)
            return NotFound("ride-not-found");

        if (ride.RiderId != authedUser.UserId)
            return BadRequest("not-rider-of-ride");

        if (ride.Status == RideStatus.Cancelled)
            return BadRequest("ride-already-cancelled");

        ride = await rideService.CancelRideAsync(cancelRide.RideId);

        if (ride == null)
            return BadRequest("failed-cancelling");

        return Ok(ride);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserViewModel>> GetUserById(string id)
    {
        if (!Guid.TryParse(id, out var parsedId))
            return BadRequest("invalid-id");

        var user = await userService.GetUserByIdAsync(parsedId);

        if (user == null)
            return NotFound("user-not-found");

        return Ok(user);
    }
}
