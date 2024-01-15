using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IRideService
{
    /// <summary>
    /// Creates a new ride
    /// </summary>
    /// <param name="ride">Ride input data</param>
    /// <param name="riderId">Rider ID</param>
    /// <returns>The newly created ride's data</returns>
    Task<RideViewModel?> CreateRideAsync(RideInputModel ride, Guid riderId);

    /// <summary>
    /// Updates the rides 
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <param name="rideStops">Collection of ride stops</param>
    /// <returns></returns>
    Task<RideViewModel?> UpdateRideStopsAsync(Guid id, ICollection<RideStopInputModel> rideStops);

    /// <summary>
    /// Marks a ride as cancelled
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <returns>The updated ride's data</returns>
    Task<RideViewModel?> CancelRideAsync(Guid id);

    /// <summary>
    /// Marks a ride as accepted
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <param name="acceptedByDriverId">ID of driver who's accepted the ride</param>
    /// <returns>The updated ride's data</returns>
    Task<RideViewModel?> MarkRideAcceptedAsync(Guid id, Guid acceptedByDriverId);

    /// <summary>
    /// Marks ride as taken off
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <returns>The updated ride's data</returns>
    Task<RideViewModel?> MarkRideTookOffAsync(Guid id);

    /// <summary>
    /// Marks ride as arrived at the destination
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <returns>The updated ride's data</returns>
    Task<RideViewModel?> MarkRideArrivedAsync(Guid id);

    /// <summary>
    /// Gets a ride by given ID
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <returns>The ride</returns>
    Task<RideViewModel?> GetRideByIdAsync(Guid id);

    /// <summary>
    /// Updates the ride's user review
    /// </summary>
    /// <param name="id">Ride ID</param>
    /// <param name="userReviewId">User review ID</param>
    /// <returns>The updated ride's data</returns>
    Task<RideViewModel?> UpdateUserReviewIdAsync(Guid id, Guid? userReviewId);

    /// <summary>
    /// Checks whether a driver has unfinished rides
    /// </summary>
    /// <param name="driverId">Driver ID</param>
    /// <returns>A boolean which tells whether a drier has unfinished rides</returns>
    Task<bool> DriverHasUnfinishedRides(Guid driverId);

    /// <summary>
    /// Gets a list of the driver's rides via a driver ID
    /// </summary>
    /// <param name="driverId">Driver ID</param>
    /// <param name="isOnGoing">Filter ongoing rides</param>
    /// <returns>The list of the driver's rides</returns>
    Task<ICollection<RideViewModel>?> GetDriverRidesAsync(Guid driverId, bool isOnGoing = false);

    /// <summary>
    /// Gets a list of the user's rides via a user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="isOnGoing">Filters ongoing rides</param>
    /// <returns>The list of the user's rides</returns>
    Task<ICollection<RideViewModel>?> GetUserRidesAsync(Guid userId, bool isOnGoing = false);

    /// <summary>
    /// Gets a list of the users' requested rides
    /// </summary>
    /// <returns>A collection if rides</returns>
    Task<ICollection<RideViewModel>> GetRequestedRidesAsync();
}