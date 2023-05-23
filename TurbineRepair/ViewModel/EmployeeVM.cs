using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class EmployeeVM : Base.ViewModel
    {

        #region Command

        /*-----------------------------------Command-------------------------------------*/

        #region UserSearch
        public ICommand UserSearch { get; }

        private bool CanUserSearchExecute(object parametr) => true;
        private void OnUserSearchExecute(object parametr)
        {
            if (SearchUser != null)
            {
                UserItem = Users.Where(x => x.Surname.StartsWith(SearchUser));
            }
            else UserItem = Users;
        }
        #endregion

        #region OpenCreateUser

        public ICommand OpenCreateUser { get; }
        private bool CanOpenCreateUserExecute(object parametr) => true;
        private void OnOpenCreateUserExecute(object parametr)
        {
            MainWindowViewModel.main.UpdateUser = null;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateEmployee();
        }

        #endregion

        #region OpenUpdateUser

        public ICommand OpenUpdateUser { get; }
        private bool CanOpenUpdateUserExecute(object parametr) => true;
        private void OnOpenUpdateUserExecute(object parametr)
        {
            if(UserUpd != null)
            {
                MainWindowViewModel.main.UpdateUser = UserUpd;
                MainVM.mainVM.MainCurrentControl = new CreateOrUpdateEmployee();
            }
            else
            {
                FailedAddOrUpdateContent = "*Не выбран сотрудник";
                ForegroundFailedMessage = -1;
            }
           
        }
        #endregion

        #region DeleteUser

        public ICommand DeleteUser { get; }
        private bool CanDeleteUserExecute(object parametr) => true;
        private async void OnDeleteUserExecute(object parametr)
        {

            if (UserUpd != null)
            {
                UserDatum delUser = UserUpd;
                delUser.DeleteUser = true;
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                ForegroundFailedMessage = 1;
                FailedAddOrUpdateContent = "Сотрудник удален";
                UserItem = MainWindowViewModel.main.UsersAll;
                UserItem = Users.Where(x => x.DeleteUser == false);
            }
            else
            {
                FailedAddOrUpdateContent = "*Не выбран сотрудник";
                ForegroundFailedMessage = -1;
            }
            
            

        }
        #endregion

        public ICommand OpenProfileEmployee { get; }
        private bool CanOpenProfileEmployeeExecute(object parametr) => true;
        private void OnOpenProfileEmployeeExecute(object parametr)
        {
            if (UserUpd != null)
            {
                MainWindowViewModel.main.ObsUser = UserUpd;
                MainVM.mainVM.MainCurrentControl = new MyProfileVM();
            }
            else
            {
                FailedAddOrUpdateContent = "*Не выбран сотрудник";
                ForegroundFailedMessage = -1;
            }
        }




        /*-----------------------------------Command-------------------------------------*/

        #endregion

        #region UserItem
        private object _userItem;
        public object UserItem
        {
            get => _userItem;
            set => Set(ref _userItem, value);
        }
        #endregion

        #region List

        /*--------------------------------------List----------------------------------*/

        public List<UserDatum> Users { get; set; }

        /*--------------------------------------List----------------------------------*/

        #endregion

        #region Property

        /*----------------------------------- Property ---------------------------*/


        private string _failedAddOrUpdateContent;
        public string FailedAddOrUpdateContent
        {
            get => _failedAddOrUpdateContent;
            set => Set(ref _failedAddOrUpdateContent, value);
        }

        private decimal _foregroundFailedMessage;
        public decimal ForegroundFailedMessage
        {
            get => _foregroundFailedMessage;
            set => Set(ref _foregroundFailedMessage, value);
        }

        #region SearchUser

        private string _searchUser;
        public string SearchUser
        {
            get => _searchUser;
            set => Set(ref  _searchUser, value);
        }

        #endregion

        #region UpdUser
        private UserDatum _userUpd;
        public UserDatum UserUpd
        {
            get => _userUpd;
            set => Set(ref _userUpd, value);
        }
        #endregion

        /*----------------------------------- Property ---------------------------*/

        #endregion

        public EmployeeVM() 
        {


            Users = MainWindowViewModel.main.UsersAll;
            UserItem = Users.Where(x => x.DeleteUser == false);

            #region Command

            /*----------------------------------- Command --------------------------------*/

            #region OpenCreateUser

            OpenCreateUser = new LambdaCommand(OnOpenCreateUserExecute, CanOpenCreateUserExecute);

            #endregion

            #region OpenUpdateUser
            OpenUpdateUser = new LambdaCommand(OnOpenUpdateUserExecute, CanOpenUpdateUserExecute);
            #endregion

            #region DeleteUser
            DeleteUser = new LambdaCommand(OnDeleteUserExecute, CanDeleteUserExecute);
            #endregion
            UserSearch = new LambdaCommand(OnUserSearchExecute, CanUserSearchExecute);
            OpenProfileEmployee = new LambdaCommand(OnOpenProfileEmployeeExecute, CanOpenProfileEmployeeExecute);

            /*----------------------------------- Command --------------------------------*/

            #endregion
        }

    }
}
