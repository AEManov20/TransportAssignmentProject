using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;
using Transport.Data;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Implementations;

internal class RideService : IRideService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public RideService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<RideViewModel?> CreateRideAsync(RideInputModel rideInput, Guid riderId)
    {
        var ride = new Ride()
        {
            RiderId = riderId
        };

        context.Rides.Add(ride);

        context.RideStops.AddRange(rideInput.RideStops
            .Select((e, i) => new RideStop()
                {
                    RideId = ride.Id,
                    AddressText = e.AddressText,
                    OrderingNumber = (short)i
                }));

        await context.SaveChangesAsync();

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<RideViewModel?> UpdateRideStopsAsync(Guid id, ICollection<RideStopInputModel> rideStops)
    {
        context.RemoveRange(context.RideStops.Where(r => r.RideId == id));
        context.AddRange(rideStops.Select((e, i) => new RideStop()
        {
            AddressText = e.AddressText,
            OrderingNumber = (short)i,
            RideId = id
        }));

        await context.SaveChangesAsync();
        
        return mapper.Map<RideViewModel>(await context.Rides
            .Include(e => e.RideStops)
            .FirstOrDefaultAsync(e => e.Id == id));
    }

    public async Task<RideViewModel?> GetRideByIdAsync(Guid id)
    {
        var ride = await context.Rides
            .Include(e => e.RideStops)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (ride == null)
            return null;

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<ICollection<RideViewModel>?> GetDriverRidesAsync(Guid driverId, bool isOnGoing = false)
    {
        var rides = context.Rides
            .Include(e => e.RideStops)
            .Where(e => e.DriverId == driverId)
            .Select(e => mapper.Map<RideViewModel>(e));

        return isOnGoing
            ? await rides.Where(e => e.Status != RideStatus.Finalized || e.Status != RideStatus.Cancelled).ToListAsync()
            : await rides.ToListAsync();
    }

    public async Task<ICollection<RideViewModel>?> GetUserRidesAsync(Guid userId, bool isOnGoing = false)
    {
        var rides = context.Rides
            .Include(e => e.RideStops)
            .Where(e => e.RiderId == userId)
            .Select(e => mapper.Map<RideViewModel>(e));

        return isOnGoing
            ? await rides.Where(e => e.Status != RideStatus.Finalized || e.Status != RideStatus.Cancelled).ToListAsync()
            : await rides.ToListAsync();
    }

    public async Task<RideViewModel?> CancelRideAsync(Guid id)
    {
        var ride = await context.Rides.FirstOrDefaultAsync(e => e.Id == id);
        
        if (ride == null)
            return null;

        ride.Status = RideStatus.Cancelled;

        context.Rides.Update(ride);

        await context.SaveChangesAsync();

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<RideViewModel?> MarkRideAcceptedAsync(Guid id, Guid acceptedByDriverId)
    {
        var ride = await context.Rides.FirstOrDefaultAsync(e => e.Id == id);

        if (ride == null)
            return null;

        ride.Status = RideStatus.Accepted;
        ride.AcceptedOn = DateTime.UtcNow;
        ride.DriverId = acceptedByDriverId;

        context.Rides.Update(ride);

        await context.SaveChangesAsync();

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<RideViewModel?> MarkRideArrivedAsync(Guid id)
    {
        var ride = await context.Rides.FirstOrDefaultAsync(e => e.Id == id);

        if (ride == null)
            return null;

        ride.Status = RideStatus.Finalized;
        ride.ArrivedOn = DateTime.UtcNow;

        context.Rides.Update(ride);

        await context.SaveChangesAsync();

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<RideViewModel?> MarkRideTookOffAsync(Guid id)
    {
        var ride = await context.Rides.FirstOrDefaultAsync(e => e.Id == id);

        if (ride == null)
            return null;

        ride.Status = RideStatus.TookOff;
        ride.TookOffOn = DateTime.UtcNow;

        context.Rides.Update(ride);

        await context.SaveChangesAsync();

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<bool> DriverHasUnfinishedRides(Guid driverId)
    {
        return await context.Rides
            .Where(x => x.Status != RideStatus.Cancelled && x.Status != RideStatus.Finalized)
            .AnyAsync();
    }

    public async Task<RideViewModel?> UpdateUserReviewIdAsync(Guid id, Guid? userReviewId)
    {
        var ride = await context.Rides.FirstOrDefaultAsync(e => e.Id == id);

        if (ride == null)
            return null;

        ride.UserReviewId = userReviewId;

        context.Rides.Update(ride);

        await context.SaveChangesAsync();

        return mapper.Map<RideViewModel>(ride);
    }

    public async Task<ICollection<RideViewModel>> GetRequestedRidesAsync()
    {
        return await context.Rides
            .Include(e => e.RideStops)
            .Where(e => e.Status == RideStatus.Requested)
            .Select(e => mapper.Map<RideViewModel>(e))
            .ToListAsync();
    }

    public async Task<string?> GetGoogleMapsLinkAsync(Guid rideId)
    {
        var ride = await GetRideByIdAsync(rideId);

        if (ride == null) return null;

        return "https://www.google.com/maps/dir/" + string.Join(
            '/', ride.RideStops
                .OrderBy(x => x.OrderingNumber)
                .Select(x => $"{x.Latitude},{x.Longitude}"));
    }
}