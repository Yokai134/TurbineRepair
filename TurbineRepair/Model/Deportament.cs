using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

public partial class Deportament
{
    public int Id { get; set; }

    public string DeportamentName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
