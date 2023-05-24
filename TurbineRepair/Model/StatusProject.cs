using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class StatusProject
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectDatum> ProjectData { get; set; } = new List<ProjectDatum>();

    public virtual ICollection<TurbineRequest> TurbineRequests { get; set; } = new List<TurbineRequest>();
}
