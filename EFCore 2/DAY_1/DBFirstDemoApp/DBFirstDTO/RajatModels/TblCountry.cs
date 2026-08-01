using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblCountry
{
    public int CountryCode { get; set; }

    public string Country { get; set; } = null!;

    public virtual ICollection<TblLocation> TblLocations { get; set; } = new List<TblLocation>();
}
