using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class TurbineImage
{
    public int Id { get; set; }

    public byte[]? ImageOne { get; set; }

    public byte[]? ImageTwo { get; set; }

    public byte[]? ImageThree { get; set; }

    public byte[]? ImageFour { get; set; }

    public virtual ICollection<Turbine> Turbines { get; set; } = new List<Turbine>();
}
