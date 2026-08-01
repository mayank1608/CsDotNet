using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CertificationManagementApp.Models;

public partial class TblCertificationProvider
{
    public string CproviderId { get; set; } = null!;

    public string Cprovider { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TblCertification> TblCertifications { get; set; } = new List<TblCertification>();
}
