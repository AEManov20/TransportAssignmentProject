using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Transport.Data.Models;

public partial class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();

    public virtual ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
}
