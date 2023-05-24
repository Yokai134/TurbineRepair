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
using TurbineRepair.Model;
using TurbineRepair.Migration;

namespace TurbineRepair.ViewModel
{
    internal class MainVM : Base.ViewModel
    {
        public static MainVM mainVM;

        #region Command
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
            BoolBackgroundProfile = false;
            BoolBackgroundChat = false;
            BoolBackgroundProject = true;
            BoolBackgroundRequest = false;
            BoolBackgroundEmployee = false;
            BoolBackgroundTurbine = false;
            BoolBackgroundCustomer = false;
        }
        #endregion

        #region OpenNotificationControl
        public ICommand OpenNotificationControl { get; }

        private bool CanOpenRequestControlExecute(object parameter) => true;
        private void OnOpenRequestControlExecute(object parameter)
        {
            MainCurrentControl = new RequestVM();
            BoolBackgroundProfile = false;
            BoolBackgroundChat = false;
            BoolBackgroundProject = false;
            BoolBackgroundRequest = true;
            BoolBackgroundEmployee = false;
            BoolBackgroundTurbine = false;
            BoolBackgroundCustomer = false;
        }
        #endregion

        #region OpenProfileControl
        public ICommand OpenProfileControl { get; }

        private bool CanOpenProfileControlExecute(object parameter) => true;
        private void OnOpenProfileControlExecute(object parameter)
        {
            MainWindowViewModel.main.ObsUser = null;
            MainCurrentControl = new MyProfileVM();
            BoolBackgroundProfile = true;
            BoolBackgroundChat = false;
            BoolBackgroundProject = false;
            BoolBackgroundRequest = false;
            BoolBackgroundEmployee = false;
            BoolBackgroundTurbine = false;
            BoolBackgroundCustomer = false;
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
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            
        }
        #endregion

        #region OpenChatControl
        public ICommand OpenChatControl { get; }
        private bool CanOpenChatControlExecute(object parameter) => true;
        private void OnOpenChatControlExecute(object parameter)
        {
            MainCurrentControl = new ChatVM();
            BoolBackgroundProfile = false;
            BoolBackgroundChat = true;
            BoolBackgroundProject = false;
            BoolBackgroundRequest = false;
            BoolBackgroundEmployee = false;
            BoolBackgroundTurbine = false;
            BoolBackgroundCustomer = false;
        }
        #endregion

        #region OpenEmployeeControl
        public ICommand OpenEmployeeControl { get; }
        private bool CanOpenEmployeeControlExecute(object parameter) => true;
        private void OnOpenEmployeeControlExecute(object parameter)
        {
            MainCurrentControl = new EmployeeVM();
            BoolBackgroundProfile = false;
            BoolBackgroundChat = false;
            BoolBackgroundProject = false;
            BoolBackgroundRequest = false;
            BoolBackgroundEmployee = true;
            BoolBackgroundTurbine = false;
            BoolBackgroundCustomer = false;
        }
        #endregion

        #region OpenTurbineControl
        public ICommand OpenTurbineControl { get; }
        private bool CanOpenTurbineControlExecute(object parameter) => true;
        private void OnOpenTurbineControlExecute(object parameter)
        {         
            MainCurrentControl = new TurbineVM();
            BoolBackgroundProfile = false;
            BoolBackgroundChat = false;
            BoolBackgroundProject = false;
            BoolBackgroundRequest = false;
            BoolBackgroundEmployee = false;
            BoolBackgroundTurbine = true;
            BoolBackgroundCustomer = false;
        }
        #endregion

        #region OpenCustomerControl
        public ICommand OpenCustomerControl { get; }
        private bool CanOpenCustomerControlExecute(object parameter) => true;
        private void OnOpenCustomerControlExecute(object parameter)
        {
            MainCurrentControl = new CustomerListVM();
            BoolBackgroundProfile = false;
            BoolBackgroundChat = false;
            BoolBackgroundProject = false;
            BoolBackgroundRequest = false;
            BoolBackgroundEmployee = false;
            BoolBackgroundTurbine = false;
            BoolBackgroundCustomer = true;
        }
        #endregion

        #endregion

        /*------------------------------------------- Command ---------------------------------------------------*/
        #endregion

        #region Prop
        /* ------------------------------------------ Property --------------------------------------------------*/

        #region MainCurrentControl

        private object _mainCurrentControl;

        public object MainCurrentControl 
        { 
            get => _mainCurrentControl;
            set => Set(ref _mainCurrentControl, value);
        
        }

        #endregion

        #region BoolBackgroundProfile
        private bool _boolBackgroundProfile = false;
        public bool BoolBackgroundProfile
        {
            get => _boolBackgroundProfile;
            set => Set(ref _boolBackgroundProfile, value);
        }
        #endregion

        #region BoolBackgroundProject
        private bool _boolBackgroundProject = false;
        public bool BoolBackgroundProject
        {
            get => _boolBackgroundProject;
            set => Set(ref _boolBackgroundProject, value);
        }
        #endregion

        #region BoolBackgroundRequest
        private bool _boolBackgroundRequest = false;
        public bool BoolBackgroundRequest
        {
            get => _boolBackgroundRequest;
            set => Set(ref _boolBackgroundRequest, value);
        }
        #endregion

        #region BoolBackgroundChat
        private bool _boolBackgroundChat = false;
        public bool BoolBackgroundChat
        {
            get => _boolBackgroundChat;
            set => Set(ref _boolBackgroundChat, value);
        }
        #endregion

        #region BoolBackgroundEmployee
        private bool _boolBackgroundEmployee = false;
        public bool BoolBackgroundEmployee
        {
            get => _boolBackgroundEmployee;
            set => Set(ref _boolBackgroundEmployee, value);
        }
        #endregion

        #region BoolBackgroundTurbine
        private bool _boolBackgroundTurbine = false;
        public bool BoolBackgroundTurbine
        {
            get => _boolBackgroundTurbine;
            set => Set(ref _boolBackgroundTurbine, value);
        }
        #endregion

        #region BoolBackgroundCustomer
        private bool _boolBackgroundCustomer = false;
        public bool BoolBackgroundCustomer
        {
            get => _boolBackgroundCustomer;
            set => Set(ref _boolBackgroundCustomer, value);
        }
        #endregion

        #region CurrentUser
        private UserDatum _currentUser = MainWindowViewModel.main.CurrentUser;
        public UserDatum CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
        #endregion


        /* ------------------------------------------ Property --------------------------------------------------*/
        #endregion


        /// <summary>
        /// Логика взаимодействия с Main.xaml
        /// </summary>
        public MainVM() 
        {
            MainCurrentControl = new ProjectVM();
            BoolBackgroundProject = true;
            mainVM = this;            

            #region Command
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
            OpenNotificationControl = new LambdaCommand(OnOpenRequestControlExecute, CanOpenRequestControlExecute);
            #endregion

            #region OpenProfileControl
            OpenProfileControl = new LambdaCommand(OnOpenProfileControlExecute, CanOpenProfileControlExecute);
            #endregion

            #region LogOut
            LogOut = new LambdaCommand(OnLogOutExecute, CanLogOutExecute);
            #endregion

            #region OpenChatControl
            OpenChatControl = new LambdaCommand(OnOpenChatControlExecute, CanOpenChatControlExecute);
            #endregion

            #region OpenEmployeeControl
            OpenEmployeeControl = new LambdaCommand(OnOpenEmployeeControlExecute, CanOpenEmployeeControlExecute);
            #endregion

            #region OpenTurbineControl
            OpenTurbineControl = new LambdaCommand(OnOpenTurbineControlExecute, CanOpenTurbineControlExecute);
            #endregion

            #region OpenCustomerControl
            OpenCustomerControl = new LambdaCommand(OnOpenCustomerControlExecute,CanOpenCustomerControlExecute);
            #endregion
            #endregion

            /*------------------------------------------- Command ---------------------------------------------------*/
            #endregion
        }


    }
}
