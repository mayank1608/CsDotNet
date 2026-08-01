using System;
using System.Collections.Generic;

namespace DBFirstDTO.RajatModels;

public partial class TblBusinessUnit
{
    public string UnitCode { get; set; } = null!;

    public string? Unit { get; set; }

    public virtual ICollection<TblEmployee> TblEmployees { get; set; } = new List<TblEmployee>();
}
