using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CertificationManagementApp.Models;

public partial class TblEmployee
{
    public int Psnumber { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? EmpLocation { get; set; }

    public string BusinessUnit { get; set; } = null!;

    public virtual TblBusinessUnit BusinessUnitNavigation { get; set; } = null!;

    public virtual TblLocation? EmpLocationNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<TblEmployeeCertification> TblEmployeeCertifications { get; set; } = new List<TblEmployeeCertification>();
}
