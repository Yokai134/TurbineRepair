using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace TurbineRepair.ViewModel
{
    internal class MainVM : Base.ViewModel
    {
        /*------------------------------------------- Command ---------------------------------------------------*/
        #region Command

        #region CloseAppCommand

        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecute(object parameter) => true;

        private void OnCloseAppCommandExecute(object parameter)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region WindowsStateCommand

        public ICommand WindowsStateCommand { get; }

        private bool CanWindowsStateCommandExecute (object parameter) => true;

        private void OnWindowsStateCommandExecute(object parameter)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        #endregion

        #region WindowsMinimizedCommadn

        public ICommand WindowsMinimizedCommadn { get; }

        private bool CanWindowsMinimizedCommadnExecute(object parameter) => true;

        private void OnWindowsMinimizedCommadnExecute(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion

        #endregion

        /*------------------------------------------- Command ---------------------------------------------------*/

        /// <summary>
        /// Логика взаимодействия с Main.xaml
        /// </summary>
        public MainVM() 
        {

            /*------------------------------------------- Command ---------------------------------------------------*/

            #region Command

            #region CloseAppCommand
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecute, CanCloseAppCommandExecute);
            #endregion

            #region WindowsStateCommand
            WindowsStateCommand = new LambdaCommand(OnWindowsStateCommandExecute, CanWindowsStateCommandExecute);
            #endregion

            #region
            WindowsMinimizedCommadn = new LambdaCommand(OnWindowsMinimizedCommadnExecute, CanWindowsMinimizedCommadnExecute);
            #endregion

            #endregion

            /*------------------------------------------- Command ---------------------------------------------------*/

        }
    }
}
