using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblLocation
{
    public int LocationId { get; set; }

    public string? Lname { get; set; }

    public int? Country { get; set; }

    public virtual TblCountry? CountryNavigation { get; set; }

    public virtual ICollection<TblEmployee> TblEmployees { get; set; } = new List<TblEmployee>();
}
