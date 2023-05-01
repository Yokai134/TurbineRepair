using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TurbineRepair.Infrastructure;


namespace TurbineRepair.ViewModel
{
    internal class PinCodeVM : Base.ViewModel
    {
        #region Command
        /*------------------------------------------- Command ---------------------------------------------------*/

        #region Command

        #region AutheticationBackCommand
        public ICommand AutheticationBackCommand { get; }

        private bool CanAutheticationBackCommandExecute(object parameter) => true;

        private void OnAutheticationBackCommandExecution(object parameter)
        {
            MainWindowViewModel.main.CurrentControl = new AutheticationVM();
        }
        #endregion

        #region VerificationPin

        public ICommand VerificationPin { get; }

        private bool CanVerificationPinExecut(object parameter) => true;
        private void OnVerificationPinExecut(object parametr)
        {
            if (MainWindowViewModel.main.CurrentUser.Pincode == Pin)
            {
                MainWindowViewModel.main.CurrentControl = new MainVM();
                Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
                Application.Current.MainWindow.Width = 900;
                Application.Current.MainWindow.Height = 550;
            }
            else ErrorMessage = "*Invalid pincode";

        }
        #endregion

        #endregion

        /*------------------------------------------- Command ---------------------------------------------------*/
        #endregion

        #region Property
        /* ------------------------------------------ Property -------------------------------------*/

        #region Pincode
        private string _pin;
        public string Pin
        {
            get => _pin; 
            set =>  Set(ref _pin,value);
        }
        #endregion

        #region ErrorMessage
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }
        #endregion

        /* ------------------------------------------ Property -------------------------------------*/
        #endregion

        /// <summary>
        /// Логика взаимодействия с Pincode.xaml
        /// </summary>
        public PinCodeVM() 
        {
            #region Command
            /*------------------------------------------- Command ---------------------------------------------------*/

            #region Command

            #region AutheticationBackCommand
            AutheticationBackCommand = new LambdaCommand(OnAutheticationBackCommandExecution, CanAutheticationBackCommandExecute);
            #endregion

            #region VerificationPinCommand
            VerificationPin = new LambdaCommand(OnVerificationPinExecut, CanVerificationPinExecut);
            #endregion

            #endregion

            /*------------------------------------------- Command ---------------------------------------------------*/
            #endregion
        }
    }
}
