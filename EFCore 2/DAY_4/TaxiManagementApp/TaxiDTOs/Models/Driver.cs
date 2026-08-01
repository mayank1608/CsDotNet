using System;
using System.Collections.Generic;

namespace TaxiDTOs.Models;

public partial class Driver
{
    public string DriverId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? DrivingLicenceNo { get; set; }

    public DateOnly Expirydate { get; set; }

    public int? Active { get; set; }

    public virtual ICollection<AllocateTaxi> AllocateTaxis { get; set; } = new List<AllocateTaxi>();
}
