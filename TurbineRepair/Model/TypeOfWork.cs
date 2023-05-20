using System;
using System.Collections.Generic;
using TurbineRepair.Model;

namespace TurbineRepair.Model;

public partial class TypeOfWork
{
    public int Id { get; set; }

    public string NameWork { get; set; } = null!;

    public virtual ICollection<ProjectDatum> ProjectData { get; set; } = new List<ProjectDatum>();
}
