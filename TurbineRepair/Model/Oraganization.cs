using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class Oraganization
{
    public int Id { get; set; }

    public string OraganizationName { get; set; } = null!;

    public string OraganizationAdres { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
