﻿using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

public partial class UserDatum
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronomyc { get; set; } = null!;

    public int? Role { get; set; }

    public int? Deportament { get; set; }

    public int? Post { get; set; }

    public string Phone { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Pincode { get; set; }
}
