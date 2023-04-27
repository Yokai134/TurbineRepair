using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.Test
{
    internal class ContractInfo
    {

        public string contactName { get; set; }
        public string imageSource { get; set; } 
        public ObservableCollection<MessageInfo> messages { get; set; }
        public string lastMessage => messages.Last().message;
    }
}
