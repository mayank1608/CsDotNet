using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CertificationManagementApp.Models;

public partial class TblBusinessUnit
{
    public string UnitCode { get; set; } = null!;

    public string? Unit { get; set; }
    [JsonIgnore]
    public virtual ICollection<TblEmployee> TblEmployees { get; set; } = new List<TblEmployee>();
}
