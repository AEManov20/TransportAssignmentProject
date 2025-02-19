﻿using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class RideStopInputModel
{
    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string? AddressText { get; set; } = string.Empty;

    [Range(-90, 90)] [Required] public double Latitude { get; set; }

    [Range(-180, 180)] [Required] public double Longitude { get; set; }
}