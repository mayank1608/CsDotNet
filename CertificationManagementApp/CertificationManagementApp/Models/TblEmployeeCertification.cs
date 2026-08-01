using System;
using System.Collections.Generic;

namespace CertificationManagementApp.Models;

public partial class TblEmployeeCertification
{
    public int RegistrationId { get; set; }

    public int? EmployeePsnumber { get; set; }

    public int? CertificationId { get; set; }

    public int? CertificationStatus { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public DateOnly? CertifiedDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual TblCertification? Certification { get; set; }

    public virtual TblCstatus? CertificationStatusNavigation { get; set; }

    public virtual TblEmployee? EmployeePsnumberNavigation { get; set; }
}
