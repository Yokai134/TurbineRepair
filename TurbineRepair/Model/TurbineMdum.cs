using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

public partial class TurbineMdum
{
    public int Id { get; set; }

    public string PowerOutput { get; set; } = null!;

    public string Fuel { get; set; } = null!;

    public string Efficiency { get; set; } = null!;

    public string HeatRate { get; set; } = null!;

    public string DriveShaftSpeed { get; set; } = null!;

    public string PressureRatio { get; set; } = null!;

    public string ExhaustMassFlow { get; set; } = null!;

    public string ExhaustTemperature { get; set; } = null!;

    public string Emissions { get; set; } = null!;

    public virtual ICollection<Turbine> Turbines { get; set; } = new List<Turbine>();
}
