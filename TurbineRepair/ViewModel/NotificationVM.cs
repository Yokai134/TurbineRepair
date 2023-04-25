using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class NotificationVM : Base.ViewModel
    {
        /*------------------------------------------- Property ---------------------------------------*/

        public ObservableCollection<NotificationInfo> Notifications { get; set; }

        /*------------------------------------------- Property ---------------------------------------*/

        public NotificationVM() 
        { 
            Notifications = new ObservableCollection<NotificationInfo>();

            for(int i = 0; i < 5; i++) 
            {
                Notifications.Add(new NotificationInfo
                {
                    nameSender = $"test{i}",
                    descriptionNotification = $"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa{i}"
                });
            }
        
        } 
    }
}
