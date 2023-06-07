using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class MyProfileVM : Base.ViewModel
    {

        #region Property
        /*---------------------------------- Property -------------------------------------*/

        private UserDatum? _userDatum;
        public UserDatum UserDatum
        {
            get => _userDatum;
            set => Set(ref _userDatum, value);
        }

        private Deportament? _deport;
        public Deportament Deport
        {
            get => _deport;
            set => Set(ref _deport, value);
        }

        private Post? _posts;
        public Post Posts
        {
            get => _posts;
            set => Set(ref _posts, value);
        }

        private bool _checkEmployee;
        public bool CheckEmployee
        {
            get => _checkEmployee;
            set => Set(ref _checkEmployee, value);
        }
        /*---------------------------------- Property -------------------------------------*/
        #endregion

        #region Command
        public ICommand BackEmployeeList { get; }
        private bool CanBackEmployeeListExecute(object parameter) => true;
        private void OnBackEmployeeListExecute(object parameter)
        {
            MainVM.mainVM.MainCurrentControl = new EmployeeVM();
            MainWindowViewModel.main.ObsUser = null;
        }
        #endregion

        public MyProfileVM() 
        {
            if (MainWindowViewModel.main.ObsUser == null)
            {
                UserDatum = MainWindowViewModel.main.CurrentUser;
                CheckEmployee = false;
            }
            else
            {
                UserDatum = MainWindowViewModel.main.ObsUser;
                CheckEmployee = true;
            }

            BackEmployeeList = new LambdaCommand(OnBackEmployeeListExecute, CanBackEmployeeListExecute);
        
        }


    }
}
