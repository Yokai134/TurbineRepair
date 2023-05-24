using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerSurname { get; set; } = null!;

    public string CustomerPatronomyc { get; set; } = null!;

    public int CustomerOrganization { get; set; }

    public bool? DeleteCustomer { get; set; }

    public virtual Oraganization CustomerOrganizationNavigation { get; set; } = null!;

    public virtual ICollection<ProjectDatum> ProjectData { get; set; } = new List<ProjectDatum>();
}
