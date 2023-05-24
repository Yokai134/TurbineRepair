using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class TurbineRequest
{
    public int TurbineRequestId { get; set; }

    public string? TurbineRequestDescription { get; set; }

    public int? TurbineRequestProject { get; set; }

    public int? TurbineRequestTypeOfWork { get; set; }

    public int? TurbineRequestStatus { get; set; }

    public int? TurbineRequestExecutor { get; set; }

    public DateOnly? TurbineRequestDataStart { get; set; }

    public DateOnly? TurbineRequestDataEnd { get; set; }

    public string? TurbineRequestName { get; set; }

    public virtual Post? TurbineRequestExecutorNavigation { get; set; }

    public virtual ProjectDatum? TurbineRequestProjectNavigation { get; set; }

    public virtual StatusProject? TurbineRequestStatusNavigation { get; set; }

    public virtual TypeOfWork? TurbineRequestTypeOfWorkNavigation { get; set; }
}
