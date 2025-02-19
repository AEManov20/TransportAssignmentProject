﻿using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class UserLoginModel
{
    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}