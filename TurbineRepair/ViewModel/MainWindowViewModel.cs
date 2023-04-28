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

namespace TurbineRepair.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public static MainWindowViewModel main;

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
