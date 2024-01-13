using Transport.BusinessLogic.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IReviewService
{
    Task<Tuple<UserReviewViewModel?, string?>> CreateReview(UserReviewInputModel userReview);

    Task<Tuple<bool, string?>> DeleteReviewById(Guid id);

    Task<UserReviewViewModel?> GetReviewById(Guid id);
}