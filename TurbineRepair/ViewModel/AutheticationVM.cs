using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Migration;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    public class AutheticationVM : Base.ViewModel
    {
        TurbinerepairContext context = new TurbinerepairContext();

        #region Command
        /*------------------------------------------- Command ---------------------------------------------------*/

        #region Command

        #region CloseAppCommadn
        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecute(object parameter) => true;

        private void OnCloseAppCommandExecution(object parameter)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region VerificationLogin
        public ICommand VerificationLogin { get; }

        private bool CanVerificationLoginExecut(object parameter) => true;
        private void OnVerificationLoginExecut(object parametr)
        {
            VerificationData(LoginApp,PasswordApp);
            if (checkData)
            {
                MainWindowViewModel.main.CurrentUser = userCurrent;
                MainWindowViewModel.main.Posts = postCurrent;
                MainWindowViewModel.main.Deport = deportamentCurrent;
                MainWindowViewModel.main.CurrentControl = new MainVM();             
                Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
                Application.Current.MainWindow.Width = 1280;
                Application.Current.MainWindow.Height = 750;
                Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                Application.Current.MainWindow.Left = 500;
            }
            else
            {
                ErrorMessage = "*Не верный логин или пароль";
            }

        }
        #endregion

        #endregion

        /*------------------------------------------- Command ---------------------------------------------------*/
        #endregion


        #region Property
        /* ---------------------------------------- Property ---------------------------------------- */

        #region Property

        private bool checkData;

        private UserDatum userCurrent;
        private Post postCurrent;
        private Deportament deportamentCurrent;

        #region Login and Password

        private string _loginApp;
        private string _passwordApp;
        public string LoginApp
        {
            get => _loginApp;
            set => Set(ref _loginApp, value);
        }

        public string PasswordApp
        {
            get => _passwordApp;
            set => Set(ref _passwordApp, value);
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

        #endregion

        /* ---------------------------------------- Property ---------------------------------------- */

        #endregion


        /// <summary>
        /// Логика взаимодействия с Authetication.xaml
        /// </summary>
        public AutheticationVM()
        {
            
            /*------------------------------------------- Command ---------------------------------------------------*/

            #region Command

            #region CloseAppCommand
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecution, CanCloseAppCommandExecute);
            #endregion

            #region VerificationLogin
            VerificationLogin = new LambdaCommand(OnVerificationLoginExecut, CanVerificationLoginExecut);
            #endregion

            #endregion

            /*------------------------------------------- Command ---------------------------------------------------*/


        }

        public bool VerificationData(string log, string pass)
        {
            try
            {
                List<UserDatum> selectUser = context.UserData.Where(x => x.Login == log && x.Password == pass).ToList();
                List<Post> posts = context.Posts.Where(x => x.Id == selectUser[0].Post).ToList();
                List<Deportament> deportaments = context.Deportaments.Where(x => x.Id == posts[0].DeportamentId).ToList();
                userCurrent = selectUser.FirstOrDefault();
                deportamentCurrent = deportaments.FirstOrDefault();
                postCurrent = posts.FirstOrDefault();
                if (userCurrent != null)
                {
                  
                    checkData = true;
                }
                else
                {
                   checkData = false;
                }
            }
            catch
            {
                checkData= false;
            }
            return checkData;
        }

    }
}
