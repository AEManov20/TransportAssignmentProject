using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;
using Transport.Data;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Implementations;

internal class ReviewService : IReviewService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public ReviewService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<UserReviewViewModel?> CreateReview(UserReviewInputModel userReviewInput)
    {
        var userReview = mapper.Map<UserReview>(userReviewInput);

        context.UserReviews.Add(userReview);

        await context.SaveChangesAsync();

        return mapper.Map<UserReviewViewModel>(userReview);
    }

    public async Task<UserReviewViewModel?> GetReviewById(Guid id)
    {
        var userReview = await context.UserReviews.FirstOrDefaultAsync(e => e.Id == id);

        if (userReview == null)
            return null;

        return mapper.Map<UserReviewViewModel>(userReview);
    }

    public async Task<bool> DeleteReviewById(Guid id)
    {
        var userReview = await context.UserReviews.FirstOrDefaultAsync(e => e.Id == id);

        if (userReview == null)
            return false;

        context.Remove(userReview);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<UserReviewViewModel>> GetDriverReviews(Guid driverId)
    {
        var driver = await context.Drivers
            .Include(e => e.UserReviewsDrivers)
            .ThenInclude(userReviewsDriver => userReviewsDriver.UserReview)
            .FirstOrDefaultAsync(e => e.Id == driverId);

        if (driver == null)
            return new List<UserReviewViewModel>();

        return driver.UserReviewsDrivers
            .Select(e => e.UserReview)
            .Select(e => mapper.Map<UserReviewViewModel>(e))
            .ToList();
    }

    public async Task<ICollection<UserReviewViewModel>> GetUserReviews(Guid userId) =>
        await context.UserReviews
            .Where(e => e.AuthorId == userId)
            .Select(e => mapper.Map<UserReviewViewModel>(e))
            .ToListAsync();

    public async Task<UserReviewViewModel?> GetRideReview(Guid rideId)
    {
        var ride = await context.Rides
            .Include(ride => ride.UserReview)
            .FirstOrDefaultAsync(e => e.Id == rideId);
        
        if (ride == null)
            return null;

        return mapper.Map<UserReviewViewModel>(ride.UserReview);
    }
}