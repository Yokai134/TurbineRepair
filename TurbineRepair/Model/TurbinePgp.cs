using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

public partial class TurbinePgp
{
    public int Id { get; set; }

    public string Weight { get; set; } = null!;

    public string Lenght { get; set; } = null!;

    public string Width { get; set; } = null!;

    public string Height { get; set; } = null!;

    public virtual ICollection<Turbine> Turbines { get; set; } = new List<Turbine>();
}
