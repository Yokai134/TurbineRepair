using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        /*----------------------------------- Property ---------------------------*/

        #endregion

        public EmployeeVM() 
        {

            UserSearch = new LambdaCommand(OnUserSearchExecute, CanUserSearchExecute);
            Users = MainWindowViewModel.main.UsersAll;
            UserItem = Users;
        } 

    }
}
