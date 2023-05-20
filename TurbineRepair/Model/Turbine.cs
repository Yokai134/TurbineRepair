using System;
using System.Collections.Generic;
using TurbineRepair.Model;

namespace TurbineRepair.Model;

public partial class Turbine
{
    public int Id { get; set; }

    public string TurbineName { get; set; } = null!;

    public int TurbineScpg { get; set; }

    public int TurbineMda { get; set; }

    public int TurbinePgp { get; set; }

    public int TurbineMdp { get; set; }

    public int TurbineImage { get; set; }

    public string TurbineDescription { get; set; } = null!;

    public bool? DeleteTurbine { get; set; }

    public virtual ICollection<ProjectDatum> ProjectData { get; set; } = new List<ProjectDatum>();

    public virtual TurbineImage TurbineImageNavigation { get; set; } = null!;

    public virtual TurbineMdum TurbineMdaNavigation { get; set; } = null!;

    public virtual TurbineMdp TurbineMdpNavigation { get; set; } = null!;

    public virtual TurbinePgp TurbinePgpNavigation { get; set; } = null!;

    public virtual TurbineScpg TurbineScpgNavigation { get; set; } = null!;
}
