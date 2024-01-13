namespace Transport.BusinessLogic.Models;

public class UserReviewViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public double Rating { get; set; }

    public string Content { get; set; } = string.Empty;

    public UserViewModel Author { get; set; }
}