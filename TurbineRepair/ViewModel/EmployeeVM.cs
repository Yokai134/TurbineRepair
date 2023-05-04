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

namespace TurbineRepair.ViewModel
{
    internal class EmployeeVM : Base.ViewModel
    {

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
            else MessageBox.Show("Не выбран пользователь","Уведомление",MessageBoxButton.OK,MessageBoxImage.Question);
           
        }

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

            UserSearch = new LambdaCommand(OnUserSearchExecute, CanUserSearchExecute);
            Users = MainWindowViewModel.main.UsersAll;
            UserItem = Users;

            #region Command

            /*----------------------------------- Command --------------------------------*/

            #region OpenCreateUser

            OpenCreateUser = new LambdaCommand(OnOpenCreateUserExecute, CanOpenCreateUserExecute);

            #endregion

            #region OpenUpdateUser
            OpenUpdateUser = new LambdaCommand(OnOpenUpdateUserExecute, CanOpenUpdateUserExecute);
            #endregion

            /*----------------------------------- Command --------------------------------*/

            #endregion
        }

    }
}
