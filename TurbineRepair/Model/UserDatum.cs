using System;
using System.Collections.Generic;

namespace TurbineRepair.Model;

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

    public string Pincode { get; set; } = null!;

    public string? Image { get; set; }

    public bool? DeleteUser { get; set; }

    public virtual ICollection<MessageList> MessageListMessageReceiptNavigations { get; set; } = new List<MessageList>();

    public virtual ICollection<MessageList> MessageListMessageSenderNavigations { get; set; } = new List<MessageList>();

    public virtual Post? PostNavigation { get; set; }

    public virtual ICollection<ProjectDatum> ProjectDatumProjectExecutorNavigations { get; set; } = new List<ProjectDatum>();

    public virtual ICollection<ProjectDatum> ProjectDatumProjectSecondExecutorNavigations { get; set; } = new List<ProjectDatum>();

    public virtual Role? RoleNavigation { get; set; }
}
