using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Models;

public class RideViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public User Rider { get; set; }

    public Driver Driver { get; set; }

    public RideStatus Status { get; set; }

    public DateTime RequestedOn { get; set; } = DateTime.UtcNow;

    public DateTime? AcceptedOn { get; set; }

    public DateTime? TookOffOn { get; set; }

    public DateTime? ArrivedOn { get; set; }

    public UserReviewViewModel? UserReview { get; set; }

    public ICollection<RideStop> RideStops { get; set; } = new List<RideStop>();
}
