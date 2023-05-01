using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls;
using TurbineRepair.ViewModel;
using TurbineRepair.Migration;
using Microsoft.EntityFrameworkCore;

namespace TurbineRepair.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public static MainWindowViewModel main;
        public static TurbinerepairContext context = new TurbinerepairContext();


        /* ---------------------------------------------------- UserControl -------------------------------- */

        #region UserControl
        private AutheticationVM autheticationVM { get; set; }
        #endregion

        /* ---------------------------------------------------- UserControl -------------------------------- */

        /* ---------------------------------------------------- Property -------------------------------- */

        #region Property

        #region CurrentControl
        private object _currentControl;

        public object CurrentControl
        {
            get => _currentControl;
            set => Set(ref _currentControl, value);
        }
        #endregion

        #region CurrentUser
        private UserDatum _currentUser;
        public UserDatum CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
        #endregion

        #region CurrentDeport
        private Deportament _deport;
        public Deportament Deport
        {
            get => _deport;
            set => Set(ref _deport, value);
        }
        #endregion

        #region CurrentPost
        private Post _posts;
        public Post Posts
        {
            get => _posts;
            set => Set(ref _posts, value);
        }
        #endregion

        #endregion

        /* ---------------------------------------------------- Property -------------------------------- */


        /* --------------------------------------------- List --------------------------------------*/

        #region List

        #region ProjectData
        private List<ProjectDatum> _projectData = context.ProjectData.ToList();
        public List<ProjectDatum> ProjectData
        {
            get => _projectData;
            set => Set(ref _projectData, value);
        }
        #endregion

        #region Customers

        private List<Customer> _customersAll = context.Customers.ToList();
        public List<Customer> CustomersAll
        {
            get=> _customersAll;
            set=> Set(ref _customersAll, value);
        }

        #endregion

        #region Turbine

        private List<Turbine> _turbinesAll = context.Turbines.ToList();

        public List<Turbine> TurbinesAll
        {
            get => _turbinesAll;
            set => Set(ref _turbinesAll, value);
        }
        #endregion

        #region Status
        private List<StatusProject> _statusesAll = context.StatusProjects.ToList();
        public List<StatusProject> StatusesAll
        {
            get => _statusesAll;
            set => Set(ref _statusesAll, value);
        }
        #endregion

        #region UserAll
        private List<UserDatum> _usersAll = context.UserData.ToList();
        public List<UserDatum> UsersAll
        {
            get => _usersAll;
            set => Set(ref _usersAll, value);
        }
        #endregion

        #endregion

        /* --------------------------------------------- List --------------------------------------*/


        /// <summary>
        /// Логика взаимодействия с MainWindow.xaml
        /// </summary>
        public MainWindowViewModel() 
        {           
            CurrentControl = new AutheticationVM();
            main = this;
        }




    }
}
