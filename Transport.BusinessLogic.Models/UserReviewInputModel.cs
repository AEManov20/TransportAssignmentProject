using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class UserReviewInputModel
{
    [Required]
    public double Rating { get; set; }

    [StringLength(500, MinimumLength = 1)] public string Content { get; set; } = string.Empty;
}