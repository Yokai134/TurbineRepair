using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

public partial class TurbineImage
{
    public int Id { get; set; }

    public string ImageOne { get; set; } = null!;

    public string ImageTwo { get; set; } = null!;

    public string ImageThree { get; set; } = null!;

    public string ImageFour { get; set; } = null!;

    public virtual ICollection<Turbine> Turbines { get; set; } = new List<Turbine>();
}
