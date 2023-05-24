using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class TurbineNotification
{
    public int TurbineNotificationId { get; set; }

    public string? TurbineNotificationText { get; set; }

    public DateTime? TurbineNotificationDateTime { get; set; }
}
