using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IReviewService
{
    /// <summary>
    /// Creates a new user review
    /// </summary>
    /// <param name="userReview">User review input data</param>
    /// <returns>The newly created review's data</returns>
    Task<UserReviewViewModel?> CreateReview(UserReviewInputModel userReview);

    /// <summary>
    /// Deletes a review via supplied ID
    /// </summary>
    /// <param name="id">The review ID</param>
    /// <returns>A boolean which tells whether the review has been deleted or not</returns>
    Task<bool> DeleteReviewById(Guid id);

    /// <summary>
    /// Gets review via supplied ID
    /// </summary>
    /// <param name="id">Review ID</param>
    /// <returns>The review to whom the ID is associated</returns>
    Task<UserReviewViewModel?> GetReviewById(Guid id);

    /// <summary>
    /// Gets a review attached to a ride
    /// </summary>
    /// <param name="rideId">Ride ID</param>
    /// <returns>A review</returns>
    Task<UserReviewViewModel?> GetRideReview(Guid rideId);

    /// <summary>
    /// Gets reviews attached to a driver
    /// </summary>
    /// <param name="driverId">Driver ID</param>
    /// <returns>A collection of reviews</returns>
    Task<ICollection<UserReviewViewModel>> GetDriverReviews(Guid driverId);

    /// <summary>
    /// Gets reviews attached to a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>A collection of reviews</returns>
    Task<ICollection<UserReviewViewModel>> GetUserReviews(Guid userId);
}