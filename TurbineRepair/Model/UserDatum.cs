﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace TurbineRepair;

public partial class UserDatum
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronomyc { get; set; } = null!;

    public int? Role { get; set; }

    public int? Post { get; set; }

    public string Phone { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Image { get; set; }

    public bool? DeleteUser { get; set; }

    public bool? IsOnline { get; set; }

    public string? Town { get; set; }

    public string? Street { get; set; }

    public string? Builder { get; set; }

    public string? UserEmail { get; set; }

    public virtual ICollection<MessageList> MessageListMessageReceiptNavigations { get; set; } = new List<MessageList>();

    public virtual ICollection<MessageList> MessageListMessageSenderNavigations { get; set; } = new List<MessageList>();

    public virtual Post? PostNavigation { get; set; }

    public virtual ICollection<ProjectDatum> ProjectDatumProjectExecutorNavigations { get; set; } = new List<ProjectDatum>();

    public virtual ICollection<ProjectDatum> ProjectDatumProjectSecondExecutorNavigations { get; set; } = new List<ProjectDatum>();

    public virtual Role? RoleNavigation { get; set; }
}
