using System;
using System.Collections.Generic;

namespace TaxiDTOs.Models;

public partial class Cab
{
    public int CabId { get; set; }

    public string Carnumber { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int? ManufactureYear { get; set; }

    public virtual ICollection<AllocateTaxi> AllocateTaxis { get; set; } = new List<AllocateTaxi>();
}
