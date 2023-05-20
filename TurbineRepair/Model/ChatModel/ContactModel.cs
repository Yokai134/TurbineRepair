using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurbineRepair.Test;

namespace TurbineRepair.Model.ChatModel
{
    internal class ContactModel
    {
        public int ReceiptId { get; set; }
        public string contactName { get; set; }
        public string imageSource { get; set; }
        public ObservableCollection<MessageModel> messages { get; set;}
        public string lastMessage => messages.Last().message;
    }
}
