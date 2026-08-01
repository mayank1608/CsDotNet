using System;
using System.Collections.Generic;

namespace TaxiDTOs.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? MapId { get; set; }

    public string Source { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public decimal? Fare { get; set; }

    public DateTime? BookingDate { get; set; }

    public virtual AllocateTaxi? Map { get; set; }
}
