using System;
using System.Collections.Generic;

namespace TaxiDTOs.Models;

public partial class AllocateTaxi
{
    public int MapId { get; set; }

    public string? DriverId { get; set; }

    public int? CabId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Cab? Cab { get; set; }

    public virtual Driver? Driver { get; set; }
}
