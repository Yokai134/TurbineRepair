using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

public partial class ProjectDatum
{
    public int Id { get; set; }

    public string ProjectName { get; set; } = null!;

    public int? ProjectExecutor { get; set; }

    public int? ProjectStatus { get; set; }

    public DateOnly ProjectDataStart { get; set; }

    public DateOnly ProjectDataEnd { get; set; }

    public int? ProjectCustomer { get; set; }

    public int ProjectTurbine { get; set; }

    public bool? DeleteProject { get; set; }

    public decimal ProjectCost { get; set; }

    public int TypeProject { get; set; }

    public int? ProjectSecondExecutor { get; set; }

    public virtual Customer? ProjectCustomerNavigation { get; set; }

    public virtual UserDatum? ProjectExecutorNavigation { get; set; }

    public virtual UserDatum? ProjectSecondExecutorNavigation { get; set; }

    public virtual StatusProject? ProjectStatusNavigation { get; set; }

    public virtual Turbine ProjectTurbineNavigation { get; set; } = null!;

    public virtual TypeOfWork TypeProjectNavigation { get; set; } = null!;
}
