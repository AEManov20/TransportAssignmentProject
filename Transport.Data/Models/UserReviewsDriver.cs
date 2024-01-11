namespace Transport.Data.Models;

public partial class UserReviewsDriver
{
    public Guid UserReviewId { get; set; }

    public Guid DriverId { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual UserReview UserReview { get; set; } = null!;
}
