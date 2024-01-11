namespace Transport.Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();

    public virtual ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
}
