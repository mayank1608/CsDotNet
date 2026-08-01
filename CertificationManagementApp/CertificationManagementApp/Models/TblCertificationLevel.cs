using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CertificationManagementApp.Models;

public partial class TblCertificationLevel
{
    public int ClevelId { get; set; }

    public string Clevel { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TblCertification> TblCertifications { get; set; } = new List<TblCertification>();
}
