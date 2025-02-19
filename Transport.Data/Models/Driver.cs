﻿namespace Transport.Data.Models;

public enum DriverAvailability : short
{
    Available,
    Driving,
    Offline
}

public partial class Driver
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DriverAvailability AvailabilityStatus { get; set; } = DriverAvailability.Offline;

    public Guid VehicleId { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();

    public virtual ICollection<UserReviewsDriver> UserReviewsDrivers { get; set; } = new List<UserReviewsDriver>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
