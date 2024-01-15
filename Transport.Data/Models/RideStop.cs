namespace Transport.Data.Models;

public partial class RideStop
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RideId { get; set; }

    public string? AddressText { get; set; } = null!;

    public short OrderingNumber { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public virtual Ride Ride { get; set; } = null!;
}
