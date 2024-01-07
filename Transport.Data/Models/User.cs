using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Transport.Data.Models;

class User : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}
