namespace Transport.Data.Models;

public partial class RideStop
{
    public Guid Id { get; set; }

    public Guid RideId { get; set; }

    public string AddressText { get; set; } = null!;

    public short OrderingNumber { get; set; }

    public virtual Ride Ride { get; set; } = null!;
}
