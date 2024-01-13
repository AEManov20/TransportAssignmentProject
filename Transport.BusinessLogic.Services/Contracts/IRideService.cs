using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public enum RideStatus : short
{
    Requested,
    Accepted,
    TookOff,
    Finalized,
    Cancelled
}

public interface IRideService
{
    Task<Tuple<Ride?, string?>> CreateRideAsync(RideInputModel ride, Guid riderId);

    Task<Ride?> CancelRideAsync(Guid id);

    Task<Ride?> MarkRideAcceptedAsync(Guid id);

    Task<Ride?> MarkRideTookOffAsync(Guid id);

    Task<Ride?> MarkRideArrivedAsync(Guid id);

    Task<Tuple<Ride?, string?>> UpdateUserReviewId(Guid id, Guid userReviewId);

    Task<ICollection<Ride>?> GetDriverRidesAsync(Guid driverId, bool isOnGoing = false);

    Task<ICollection<Ride>?> GetUserRidesAsync(Guid userId, bool isOnGoing = false);
}