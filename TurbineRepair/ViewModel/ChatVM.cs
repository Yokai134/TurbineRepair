using Newtonsoft.Json.Bson;
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

        DispatcherTimer timer = new DispatcherTimer();
        TurbinerepairContext context = new TurbinerepairContext();

        #region Command

        #region ContactSearch
        public ICommand ContactSearch { get; }

        private bool CanContactSearchExecute(object parametr) => true;
        private void OnContactSearchExecute(object parametr)
        {
            if (SearchUser != null)
            {
               ContactItem = ContactList.Where(x => x.Surname.StartsWith(SearchUser) && x.DeleteUser == false).ToList();
            }
            else if (SearchUser == "") ContactItem = ContactList;
        }
        #endregion

        #region SendMessage
        public ICommand SendMessage { get; }
        private bool CanSendMessageExecute(object parametr) => true;
        private void OnSendMessageExecute(object parametr)
        {
            try
            {
                if(SelectContact != null)
                {
                    if (Message != "")
                    {
                        MessageList newMessage = new MessageList()
                        {
                            MessageSender = CurrentUser.Id,
                            MessageReceipt = SelectContact.Id,
                            MessageText = Message,
                            MessgeTime = DateTime.Now

                        };
                        MainWindowViewModel.context.MessageLists.Add(newMessage);
                        MainWindowViewModel.context.SaveChanges();
                        Message = "";
                    }
                    
                }
                else
                {
                    Message = "Выбирете получателя";
                }
             
            }
            catch
            {

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

        #region Object
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

        private UserDatum? _selectContact;
        public UserDatum? SelectContact
        {
            get => _selectContact;
            set 
            {
                Set(ref _selectContact, value);
                LoadMessage(_selectContact);
            }

        }

        private ObservableCollection<MessageItem> _messageItems;
        public ObservableCollection<MessageItem> MessageItems
        {
            get => _messageItems;
            set => Set(ref _messageItems, value);
        }

        private UserDatum? _currentUser = MainWindowViewModel.main.CurrentUser;
        public UserDatum? CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }

        private string _statusUser;
        public string StatusUser 
        { 
            get => _statusUser; 
            set => Set(ref _statusUser, value); 
        }
        #endregion




        /// <summary>
        /// Логика взаимодействия с Chat.xaml
        /// </summary>
        public ChatVM()
        {
            if(CurrentUser.IsOnline == true) 
            {
                StatusUser = "В сети";
            }
            else StatusUser = "Не сети";
            ContactList = MainWindowViewModel.main.UsersAll.Where(x=>x.Id != CurrentUser.Id && x.DeleteUser == false).ToList();
            ContactItem = ContactList;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += RefreshMessage;
            timer.Start();
            #region Command

            #region ContactSearch
            ContactSearch = new LambdaCommand(OnContactSearchExecute, CanContactSearchExecute);
            SendMessage = new LambdaCommand(OnSendMessageExecute, CanSendMessageExecute);
            #endregion

            #endregion
       

        }

        private void LoadMessage(UserDatum receiptUser) 
        {
            Message = string.Empty;
            timer.Stop();
            MessageList = context.MessageLists.ToList();
            MessageItems = new ObservableCollection<MessageItem>();
            var messageLists = MessageList.Where(x=>x.MessageSender == CurrentUser.Id && x.MessageReceipt == receiptUser.Id 
            || x.MessageReceipt == CurrentUser.Id && x.MessageSender == receiptUser.Id).OrderBy(x=>x.MessgeTime).ToList();
            foreach (var item in messageLists)
            {
                if(item.MessageSender == CurrentUser.Id) 
                {
                    MessageItems.Add(new MessageItem(){
                        MessageListId = item.MessageListId,
                        MessageSender = item.MessageSender,
                        MessageReceipt = item.MessageReceipt,
                        MessageText = item.MessageText,
                        MessgeTime = Convert.ToDateTime(item.MessgeTime.ToString("dd/MM/yyyy HH:mm")),
                        MessageReceiptName = receiptUser.Name,
                        MessageSenderName = CurrentUser.Name,
                        MessageReceiptImage = receiptUser.Image,
                        MessageSenderImage = CurrentUser.Image,
                        IsNative = true
                    });
                }
                if (item.MessageSender != CurrentUser.Id)
                {
                    MessageItems.Add(new MessageItem()
                    {
                        MessageListId = item.MessageListId,
                        MessageSender = item.MessageSender,
                        MessageReceipt = item.MessageReceipt,
                        MessageText = item.MessageText,
                        MessgeTime = Convert.ToDateTime(item.MessgeTime.ToString("dd/MM/yyyy HH:mm")),
                        MessageReceiptName = receiptUser.Name,
                        MessageSenderName = CurrentUser.Name,
                        MessageReceiptImage = receiptUser.Image,
                        MessageSenderImage = CurrentUser.Image,
                        IsNative = false
                    });
                }

            }
            timer.Start();

        }

        private void RefreshMessage(object sender, object e)
        {
            if (SelectContact != null)
            {
                MessageItems.Clear();
                MessageList = context.MessageLists.ToList();
                MessageItems = new ObservableCollection<MessageItem>();
                var messageLists = MessageList.Where(x => x.MessageSender == CurrentUser.Id && x.MessageReceipt == SelectContact.Id
                || x.MessageReceipt == CurrentUser.Id && x.MessageSender == SelectContact.Id).OrderBy(x => x.MessgeTime).ToList();
                foreach (var item in messageLists)
                {
                    if (item.MessageSender == CurrentUser.Id)
                    {
                        MessageItems.Add(new MessageItem()
                        {
                            MessageListId = item.MessageListId,
                            MessageSender = item.MessageSender,
                            MessageReceipt = item.MessageReceipt,
                            MessageText = item.MessageText,
                            MessgeTime = item.MessgeTime,
                            MessageReceiptName = SelectContact.Name,
                            MessageSenderName = CurrentUser.Name,
                            MessageReceiptImage = SelectContact.Image,
                            MessageSenderImage = CurrentUser.Image,
                            IsNative = true
                        });
                    }
                    if (item.MessageSender != CurrentUser.Id)
                    {
                        MessageItems.Add(new MessageItem()
                        {
                            MessageListId = item.MessageListId,
                            MessageSender = item.MessageSender,
                            MessageReceipt = item.MessageReceipt,
                            MessageText = item.MessageText,
                            MessgeTime = item.MessgeTime,
                            MessageReceiptName = SelectContact.Name,
                            MessageSenderName = CurrentUser.Name,
                            MessageReceiptImage = SelectContact.Image,
                            MessageSenderImage = CurrentUser.Image,
                            IsNative = false
                        });
                    }

                }
            }

        }

    

    }
}
