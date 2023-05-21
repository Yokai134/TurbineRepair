using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.Model.ChatModel
{
    internal class MessageItem
    {
        public int MessageListId { get; set; }

        public int MessageSender { get; set; }

        public int MessageReceipt { get; set; }

        public DateTime MessgeTime { get; set; }

        public string MessageText { get; set; } = null!;

        public string MessageReceiptName { get; set; } = null!;

        public string MessageSenderName { get; set; } = null!;

        public string MessageReceiptImage { get; set; } = null!;

        public string MessageSenderImage { get; set; } = null!;
        public bool IsNative { get; set; }
    }
}
