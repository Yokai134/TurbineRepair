using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.Model.ChatModel
{
    internal class MessageModel
    {
        public string contactName { get; set; }
        public string imageSource { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }

    }
}
