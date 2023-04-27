using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.Test
{
    internal class MessageInfo
    {
        public string contactName { get; set; }
        public string contactColor { get; set; }
        public string imageSource { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }
        public bool isNativeOrigin { get;set; }
        public bool firstMessage { get; set; }  
    }
}
