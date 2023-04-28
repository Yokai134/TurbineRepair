using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls;
using TurbineRepair.Model;
using TurbineRepair.Migration;

namespace TurbineRepair.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public static MainWindowViewModel main;
        public static TurbinerepairContext context;


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
