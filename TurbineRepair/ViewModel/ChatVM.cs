using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Migration;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class ChatVM : Base.ViewModel
    {
        TurbinerepairContext context = new TurbinerepairContext();

        /*--------------------------------------------------- Command -------------------------------------------*/

        #region Command

        #region ContactSearch
        public ICommand ContactSearch { get; }

        private bool CanContactSearchExecute(object parametr) => true;
        private void OnContactSearchExecute(object parametr)
        {
            if (SearchUser != null)
            {
                ContactItem = Contact.Where(x => x.contactName.StartsWith(SearchUser));
            }
            else ContactItem = Contact;
        }
        #endregion      

        #endregion

        /*--------------------------------------------------- Command -------------------------------------------*/

        /*--------------------------------------------------- Property ------------------------------------------*/

        #region ObsCollection
        public ObservableCollection<ContractInfo> Contact { get; set; }
        public ObservableCollection<MessageInfo> Messages { get; set; }

        public List<UserDatum> User { get; set; }
        #endregion

        #region ContactItem
        private object _contactItem;
        public object ContactItem
        {
            get => _contactItem;
            set => Set(ref _contactItem, value);
        }
        #endregion

        #region SearchUser
        private string _searchUser;
        public string SearchUser
        {
            get => _searchUser;
            set => Set(ref _searchUser, value);
        }
        #endregion

        #region Message
        private string _message;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }
        #endregion

        #region SelectedContact
        private ContractInfo _selectedContact;
        public ContractInfo SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }
        #endregion


        /*--------------------------------------------------- Property ------------------------------------------*/


        /// <summary>
        /// 
        /// </summary>
        public ChatVM()
        {
            /*--------------------------------------------------- Command -------------------------------------------*/

            #region Command

            #region ContactSearch
            ContactSearch = new LambdaCommand(OnContactSearchExecute, CanContactSearchExecute);
            #endregion

            #endregion

            /*--------------------------------------------------- Command -------------------------------------------*/
            Contact = new ObservableCollection<ContractInfo>();
            Messages = new ObservableCollection<MessageInfo>();
            User = context.UserData.ToList();

            Messages.Add(new MessageInfo()
            {
                contactName = "test",
                contactColor = "#FFFF00",
                imageSource = "https://i.imgur.com/xI3Imav.jpeg",
                message = "testMessage",
                time = DateTime.Now,
                isNativeOrigin = false,
                firstMessage = true
            });


            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageInfo()
                {
                    contactName = "test",
                    contactColor = "#FFFF00",
                    imageSource = "https://i.imgur.com/xI3Imav.jpeg",
                    message = "testMessage",
                    time = DateTime.Now,
                    isNativeOrigin = true,
                });
            }

            Messages.Add(new MessageInfo()
            {
                contactName = "me",
                contactColor = "#FFFF00",
                imageSource = "https://i.imgur.com/xI3Imav.jpeg",
                message = "testMessage",
                time = DateTime.Now,
                isNativeOrigin = true,
            });


            for (int i = 0; i < User.Count; i++)
            {
                Contact.Add(new ContractInfo()
                {
                    contactName = User[i].Name,
                    imageSource = User[i].Image,
                    messages = Messages
                });
            }


            ContactItem = Contact;
        }

    }
}
