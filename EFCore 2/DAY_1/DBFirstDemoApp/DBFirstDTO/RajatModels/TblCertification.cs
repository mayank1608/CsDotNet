using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblCertification
{
    public int CertificationId { get; set; }

    public string Title { get; set; } = null!;

    public int? CertificationLevel { get; set; }

    public string? CertificationProvider { get; set; }

    public decimal? Price { get; set; }

    public virtual TblCertificationLevel? CertificationLevelNavigation { get; set; }

    public virtual TblCertificationProvider? CertificationProviderNavigation { get; set; }

    public virtual ICollection<TblEmployeeCertification> TblEmployeeCertifications { get; set; } = new List<TblEmployeeCertification>();
}
