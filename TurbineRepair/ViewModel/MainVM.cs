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
using TurbineRepair.View.ContentGUI.MainContentGUI;

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

        #region OpenProjectControl
        public ICommand OpenProjectControl { get; }

        private bool CanOpenProjectControlExecute(object parameter) => true;
        private void OnOpenProjectControlExecute(object parameter)
        {
            MainCurrentControl = new ProjectVM();
        }
        #endregion

        #region OpenNotificationControl
        public ICommand OpenNotificationControl { get; }

        private bool CanOpenNotificationControlExecute(object parameter) => true;
        private void OnOpenNotificationControlExecute(object parameter)
        {
            MainCurrentControl = new NotificationVM();
        }
        #endregion

        #region OpenProfileControl
        public ICommand OpenProfileControl { get; }

        private bool CanOpenProfileControlExecute(object parameter) => true;
        private void OnOpenProfileControlExecute(object parameter)
        {
            MainCurrentControl = new MyProfileVM();
        }
        #endregion

        #region LogOut
        public ICommand LogOut { get; }

        private bool CanLogOutExecute(object parameter) => true;
        private void OnLogOutExecute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите выйти, все не сохраненные данные будут утеряны?", "Уведомление",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.OK:
                    MainWindowViewModel.main.CurrentControl = new AutheticationVM();
                    Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    Application.Current.MainWindow.SizeToContent = SizeToContent.WidthAndHeight;
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            
        }
        #endregion

        #endregion

        /*------------------------------------------- Command ---------------------------------------------------*/


        /* ------------------------------------------ Property --------------------------------------------------*/

        #region MainCurrentControl

        private object _mainCurrentControl;

        public object MainCurrentControl 
        { 
            get => _mainCurrentControl;
            set => Set(ref _mainCurrentControl, value);
        
        }

        #endregion

        /* ------------------------------------------ Property --------------------------------------------------*/



        /// <summary>
        /// Логика взаимодействия с Main.xaml
        /// </summary>
        public MainVM() 
        {
            MainCurrentControl = new MyProfileVM();

            /*------------------------------------------- Command ---------------------------------------------------*/

            #region Command

            #region CloseAppCommand
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecute, CanCloseAppCommandExecute);
            #endregion

            #region WindowsStateCommand
            WindowsStateCommand = new LambdaCommand(OnWindowsStateCommandExecute, CanWindowsStateCommandExecute);
            #endregion

            #region WindowsMinimizedCommadn
            WindowsMinimizedCommadn = new LambdaCommand(OnWindowsMinimizedCommadnExecute, CanWindowsMinimizedCommadnExecute);
            #endregion

            #region OpenProjectControl
            OpenProjectControl = new LambdaCommand(OnOpenProjectControlExecute, CanOpenProjectControlExecute);
            #endregion

            #region OpenNotificationControl
            OpenNotificationControl = new LambdaCommand(OnOpenNotificationControlExecute, CanOpenNotificationControlExecute);
            #endregion

            #region OpenProfileControl
            OpenProfileControl = new LambdaCommand(OnOpenProfileControlExecute, CanOpenProfileControlExecute);
            #endregion

            #region LogOut
            LogOut = new LambdaCommand(OnLogOutExecute, CanLogOutExecute);
            #endregion

            #endregion

            /*------------------------------------------- Command ---------------------------------------------------*/

        }
    }
}
