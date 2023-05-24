using System;
using System.Collections.Generic;

namespace TurbineRepair;

public partial class MessageList
{
    public int MessageListId { get; set; }

    public int MessageSender { get; set; }

    public int MessageReceipt { get; set; }

    public DateTime MessgeTime { get; set; }

    public string MessageText { get; set; } = null!;

    public virtual UserDatum MessageReceiptNavigation { get; set; } = null!;

    public virtual UserDatum MessageSenderNavigation { get; set; } = null!;
}
