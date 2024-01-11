namespace Transport.Data.Models;

public partial class Ride
{
    public Guid Id { get; set; }

    public Guid RiderId { get; set; }

    public Guid DriverId { get; set; }

    public short Status { get; set; }

    public DateTime RequestedOn { get; set; }

    public Guid? UserReviewId { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual ICollection<RideStop> RideStops { get; set; } = new List<RideStop>();

    public virtual User Rider { get; set; } = null!;

    public virtual UserReview? UserReview { get; set; }
}
