using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblCstatus
{
    public int CstatusId { get; set; }

    public string Cstatus { get; set; } = null!;

    public virtual ICollection<TblEmployeeCertification> TblEmployeeCertifications { get; set; } = new List<TblEmployeeCertification>();
}
