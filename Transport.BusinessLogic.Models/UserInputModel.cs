using System.ComponentModel.DataAnnotations;
using Transport.BusinessLogic.Models.Validators;

namespace Transport.BusinessLogic.Models;

public class UserInputModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(320, MinimumLength = 1)]
    [BetterEmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(42, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}