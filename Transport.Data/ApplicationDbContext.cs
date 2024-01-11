using Microsoft.EntityFrameworkCore;
using Transport.Data.Models;

namespace Transport.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Ride> Rides { get; set; }

    public virtual DbSet<RideStop> RideStops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserReview> UserReviews { get; set; }

    public virtual DbSet<UserReviewsDriver> UserReviewsDrivers { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Drivers_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Driver)
                .HasForeignKey<Driver>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Drivers_Users_FK");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Drivers_Vehicles_FK");
        });

        modelBuilder.Entity<Ride>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rides_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RequestedOn).HasPrecision(0);

            entity.HasOne(d => d.Driver).WithMany(p => p.Rides)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Rides_Drivers_FK");

            entity.HasOne(d => d.Rider).WithMany(p => p.Rides)
                .HasForeignKey(d => d.RiderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Rides_Users_FK");

            entity.HasOne(d => d.UserReview).WithMany(p => p.Rides)
                .HasForeignKey(d => d.UserReviewId)
                .HasConstraintName("Rides_UserReviews_FK");
        });

        modelBuilder.Entity<RideStop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RideStops_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddressText)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Ride).WithMany(p => p.RideStops)
                .HasForeignKey(d => d.RideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RideStops_Rides_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserReviews_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.UserReviews)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserReviews_Users_FK");
        });

        modelBuilder.Entity<UserReviewsDriver>(entity =>
        {
            entity.HasKey(e => new { e.UserReviewId, e.DriverId }).HasName("UserReviewsDrivers_PK");

            entity.HasIndex(e => e.UserReviewId, "UserReviewsDrivers_UN").IsUnique();

            entity.HasOne(d => d.Driver).WithMany(p => p.UserReviewsDrivers)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserReviewsDrivers_Drivers_FK");

            entity.HasOne(d => d.UserReview).WithOne(p => p.UserReviewsDriver)
                .HasForeignKey<UserReviewsDriver>(d => d.UserReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserReviewsDrivers_UserReviews_FK");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Vehicles_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegisteredInCountry)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
