using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;

namespace Transport.WebHost.Controllers;

[Authorize]
[Route("api/driver")]
[ApiController]
public class DriverController : Controller
{
    private readonly IDriverService driverService;
    private readonly IAuthedUser authedUser;
    private readonly IRideService rideService;

    public DriverController(IDriverService driverService, IAuthedUser authedUser, IRideService rideService)
    {
        this.driverService = driverService;
        this.authedUser = authedUser;
        this.rideService = rideService;
    }

    [HttpPost("@me")]
    public async Task<ActionResult<DriverViewModel>> RegisterSelfDriverAsync([FromBody] VehicleInputModel vehicleInput)
    {
        if (await driverService.GetDriverById(authedUser.UserId) != null)
            return Conflict("driver-already-exists");

        var driver = await driverService.CreateDriverAsync(authedUser.UserId, vehicleInput);

        if (driver == null)
            return BadRequest("failed-registering");

        return Ok(driver);
    }

    [HttpDelete("@me")]
    public async Task<IActionResult> UnregisterSelfDriverAsync()
    {
        var driver = driverService.GetDriverById(authedUser.UserId);

        if (driver == null)
            return NotFound("driver-not-found");

        if (await rideService.DriverHasUnfinishedRides(authedUser.UserId))
            return BadRequest("unfinished-rides");

        if (!await driverService.DeleteDriverById(authedUser.UserId))
            return BadRequest("failed-unregistering");

        return Ok();
    }

    [HttpPost("driver/@me/accept")]
    public async Task<IActionResult> AcceptRideAsync([FromQuery] Guid rideId)
    {
        if (await driverService.GetDriverById(authedUser.UserId) == null)
            return NotFound("driver-not-found");

        var ride = await rideService.GetRideByIdAsync(rideId);

        if (ride == null)
            return NotFound("ride-not-found");

        if (ride.DriverId != null)
            return BadRequest("ride-already-marked");

        if (await rideService.MarkRideAcceptedAsync(rideId, authedUser.UserId) == null)
            return BadRequest("failed-marking");

        await driverService
            .UpdateDriverAvailability(authedUser.UserId, Data.Models.DriverAvailability.Driving);

        return Ok();
    }

    [HttpPost("driver/@me/cancel")]
    public async Task<IActionResult> CancelRideAsync([FromQuery] Guid rideId)
    {
        var ride = await rideService.GetRideByIdAsync(rideId);

        if (ride == null)
            return NotFound("ride-not-found");

        if (ride.DriverId == null)
            return BadRequest("ride-not-accepted");

        if (ride.DriverId != authedUser.UserId)
            return BadRequest("not-driver-of-ride");

        if (ride.Status == Data.Models.RideStatus.Cancelled)
            return BadRequest("ride-already-marked");

        if (await rideService.CancelRideAsync(rideId) == null)
            return BadRequest("failed-marking");

        return Ok();
    }

    [HttpPost("driver/@me/start")]
    public async Task<IActionResult> StartRideAsync([FromQuery] Guid rideId)
    {
        var ride = await rideService.GetRideByIdAsync(rideId);

        if (ride == null)
            return NotFound("ride-not-found");

        if (ride.DriverId == null)
            return BadRequest("ride-not-accepted");

        if (ride.DriverId != authedUser.UserId)
            return BadRequest("not-driver-of-ride");

        if (ride.Status != Data.Models.RideStatus.Cancelled)
            return BadRequest("ride-cancelled");

        if (ride.TookOffOn != null)
            return BadRequest("ride-already-marked");

        if (await rideService.MarkRideTookOffAsync(rideId) == null)
            return BadRequest("failed-marking");

        return Ok();
    }

    [HttpPost("driver/@me/finalize")]
    public async Task<IActionResult> FinalizeRideAsync([FromQuery] Guid rideId)
    {
        var ride = await rideService.GetRideByIdAsync(rideId);

        if (ride == null)
            return NotFound("ride-not-found");

        if (ride.DriverId == null)
            return BadRequest("ride-not-accepted");

        if (ride.DriverId != authedUser.UserId)
            return BadRequest("not-driver-of-ride");

        if (ride.Status != Data.Models.RideStatus.Cancelled)
            return BadRequest("ride-cancelled");

        if (ride.TookOffOn == null)
            return BadRequest("ride-not-started");

        if (ride.ArrivedOn != null)
            return BadRequest("ride-already-marked");

        if (await rideService.MarkRideArrivedAsync(rideId) == null)
            return BadRequest("failed-marking");

        await driverService
            .UpdateDriverAvailability(authedUser.UserId, Data.Models.DriverAvailability.Offline);

        return Ok();
    }

    [HttpGet("@me")]
    public async Task<ActionResult<DriverViewModel>> GetSelfDriverAsync()
    {
        var driver = await driverService.GetDriverById(authedUser.UserId);
        
        if (driver == null)
            return NotFound("driver-not-found");

        return Ok(driver);
    }

    [HttpGet("@me/rides")]
    public async Task<ActionResult<ICollection<RideViewModel>>> GetSelfDriverRidesAsync()
    {
        var rides = await rideService.GetDriverRidesAsync(authedUser.UserId);

        if (rides == null)
            return NotFound("driver-not-found");

        return Ok(rides);
    }

    [HttpGet("@me/rides/ongoing")]
    public async Task<ActionResult<ICollection<RideViewModel>>> GetSelfDriverOngoingRidesAsync()
    {
        var rides = await rideService.GetDriverRidesAsync(authedUser.UserId, true);

        if (rides == null)
            return NotFound("driver-not-found");

        return Ok(rides);
    }

    [HttpPut("@me/vehicle")]
    public async Task<ActionResult<VehicleViewModel>> UpdateDriverVehicle([FromBody] VehicleInputModel vehicle)
    {
        var driver = await driverService.GetDriverById(authedUser.UserId);
        
        if (driver == null)
            return NotFound("driver-not-found");

        var result = await driverService.UpdateDriverVehicleAsync(driver.Id, vehicle);
        
        if (result == null)
            return BadRequest("failed-updating");
        
        return Ok(result);
    }

}
