using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CertificationManagementApp.Models;

public partial class TblCstatus
{
    public int CstatusId { get; set; }

    public string Cstatus { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TblEmployeeCertification> TblEmployeeCertifications { get; set; } = new List<TblEmployeeCertification>();
}
