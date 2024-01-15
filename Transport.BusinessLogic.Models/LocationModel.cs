using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class LocationModel
{
    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
}