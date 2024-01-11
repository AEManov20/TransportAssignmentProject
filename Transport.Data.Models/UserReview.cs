namespace Transport.Data.Models;

public partial class UserReview
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public double Rating { get; set; }

    public string Content { get; set; } = null!;

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();

    public virtual UserReviewsDriver? UserReviewsDriver { get; set; }
}
