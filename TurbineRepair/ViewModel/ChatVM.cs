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
using TurbineRepair.Model;
using TurbineRepair.Model.ChatModel;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class ChatVM : Base.ViewModel
    {

        #region Command

        #region ContactSearch
        public ICommand ContactSearch { get; }

        private bool CanContactSearchExecute(object parametr) => true;
        private void OnContactSearchExecute(object parametr)
        {
            if (SearchUser != null)
            {
               
            }
        }
        #endregion

        #region SendMessage
        public ICommand SendMessage { get; }
        private bool CanSendMessageExecute(object parametr) => true;
        private async void OnSendMessageExecute(object parametr)
        {
            if(Message != null) 
            { 
                Model.MessageList newMessage = new MessageList() 
                { 
                    MessageSender = CurrentUser.Id,
                    MessageReceipt = SelectedContact.ReceiptId,
                    MessageText = Message,
                    MessgeTime = DateTime.Now
                };
                MessageObs.Add(new MessageModel()
                {
                    message = Message,                   
                });
                MainWindowViewModel.context.MessageLists.Add(newMessage);
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                LoadContact(ContactList, MessageList);
            }
        }
        #endregion

        #endregion

        #region Property

        #region List
        private List<UserDatum> _contactList;
        public List<UserDatum> ContactList
        {
            get => _contactList;
            set => Set(ref _contactList, value);
        }

        private List<MessageList> _messageList;
        public List<MessageList> MessageList
        {
            get => _messageList;
            set => Set(ref _messageList, value);
        }
        #endregion

        #region ChatLists
        private List<ContactModel> _contactModels;
        public List<ContactModel> ContactModels
        {
            get => _contactModels;
            set => Set(ref _contactModels, value);
        }

        #region ObsCollection
        public ObservableCollection<ContactModel> ContactObs { get; set; }

        public ObservableCollection<MessageModel> MessageObs { get; set; }

        #endregion

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
        private ContactModel _selectedContact;
        public ContactModel SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }
        #endregion

        private UserDatum? _currentUser = MainWindowViewModel.main.CurrentUser;
        public UserDatum? CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
        #endregion



        /// <summary>
        /// Логика взаимодействия с Chat.xaml
        /// </summary>
        public ChatVM()
        {

            ContactList = MainWindowViewModel.main.UsersAll.ToList();


            MessageList = MainWindowViewModel.main.MessageLists.ToList();


            LoadContact(ContactList, MessageList);

            #region Command

            #region ContactSearch
            ContactSearch = new LambdaCommand(OnContactSearchExecute, CanContactSearchExecute);
            SendMessage = new LambdaCommand(OnSendMessageExecute, CanSendMessageExecute);
            #endregion

            #endregion
       

        }

        private void LoadContact(List<UserDatum> usersList , List<MessageList> messageLists) 
        {

            ContactObs = new ObservableCollection<ContactModel>();
            foreach (var user in usersList)
            {
                if(user.Id != CurrentUser.Id)
                {
                    
                    MessageObs = new ObservableCollection<MessageModel>();

                    var messages = messageLists.Where(x => x.MessageSender == CurrentUser.Id && x.MessageReceipt == user.Id).ToList();
                    foreach (var messageItems in messages)
                    {

                        MessageObs.Add(new MessageModel
                        {
                            contactName = user.Name,
                            imageSource = user.Image,
                            message = messageItems.MessageText,
                            time = messageItems.MessgeTime
                        });
                    }

                    if(MessageObs.Count > 0) 
                    {
                        ContactObs.Add(new ContactModel()
                        {
                            ReceiptId = user.Id,
                            contactName = user.Name,
                            imageSource = user.Image,
                            messages = MessageObs,

                        });
                    }
                    else
                    {
                        ContactObs.Add(new ContactModel()
                        {
                            ReceiptId = user.Id,
                            contactName = user.Name,
                            imageSource = user.Image,
                            messages = null,

                        });
                    }


                }
            }

        }

    }
}
