using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblCertificationProvider
{
    public string CproviderId { get; set; } = null!;

    public string Cprovider { get; set; } = null!;

    public virtual ICollection<TblCertification> TblCertifications { get; set; } = new List<TblCertification>();
}
