using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class Post
{
    public int Id { get; set; }

    public int DeportamentId { get; set; }

    public string PostName { get; set; } = null!;

    public virtual Deportament Deportament { get; set; } = null!;

    public virtual ICollection<TurbineRequest> TurbineRequests { get; set; } = new List<TurbineRequest>();

    public virtual ICollection<UserDatum> UserData { get; set; } = new List<UserDatum>();
}
