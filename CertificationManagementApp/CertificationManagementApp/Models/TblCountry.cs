using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CertificationManagementApp.Models;

public partial class TblCountry
{
    public int CountryCode { get; set; }

    public string Country { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TblLocation> TblLocations { get; set; } = new List<TblLocation>();
}
