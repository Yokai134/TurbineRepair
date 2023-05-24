using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class TurbineScpg
{
    public int Id { get; set; }

    public string PowerOutput { get; set; } = null!;

    public string Fuel { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public string GrossEfficiency { get; set; } = null!;

    public string HeatRate { get; set; } = null!;

    public string TurbineSpeed { get; set; } = null!;

    public string PressureRatio { get; set; } = null!;

    public string ExhaustMassFlow { get; set; } = null!;

    public string ExhaustTemperature { get; set; } = null!;

    public string? Emissions { get; set; }

    public virtual ICollection<Turbine> Turbines { get; set; } = new List<Turbine>();
}
