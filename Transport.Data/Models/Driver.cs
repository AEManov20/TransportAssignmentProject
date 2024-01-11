namespace Transport.Data.Models;

public partial class Driver
{
    public Guid Id { get; set; }

    public short AvailabilityStatus { get; set; }

    public Guid VehicleId { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();

    public virtual ICollection<UserReviewsDriver> UserReviewsDrivers { get; set; } = new List<UserReviewsDriver>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
