using System;
using System.Collections.Generic;
using TurbineRepair.Model;

namespace TurbineRepair.Model;

public partial class StatusProject
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectDatum> ProjectData { get; set; } = new List<ProjectDatum>();
}
