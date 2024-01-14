namespace Transport.Data.Models;

public enum RideStatus : short
{
    Requested,
    Accepted,
    TookOff,
    Finalized,
    Cancelled
}

public partial class Ride
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RiderId { get; set; }

    public Guid? DriverId { get; set; }

    public RideStatus Status { get; set; }

    public DateTime RequestedOn { get; set; } = DateTime.UtcNow;

    public DateTime? AcceptedOn { get; set; }

    public DateTime? TookOffOn { get; set; }

    public DateTime? ArrivedOn { get; set; }

    public Guid? UserReviewId { get; set; }

    public virtual Driver? Driver { get; set; } = null!;

    public virtual ICollection<RideStop> RideStops { get; set; } = new List<RideStop>();

    public virtual User Rider { get; set; } = null!;

    public virtual UserReview? UserReview { get; set; }
}
