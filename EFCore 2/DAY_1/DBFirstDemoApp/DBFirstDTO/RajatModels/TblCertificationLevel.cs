using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblCertificationLevel
{
    public int ClevelId { get; set; }

    public string Clevel { get; set; } = null!;

    public virtual ICollection<TblCertification> TblCertifications { get; set; } = new List<TblCertification>();
}
