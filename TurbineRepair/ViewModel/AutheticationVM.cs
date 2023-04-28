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
    internal class AutheticationVM : Base.ViewModel
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
            try
            {
                List<UserDatum> selectUser = context.UserData.Where(x => x.Login == LoginApp && x.Password == PasswordApp).ToList();
                List<Post> posts = context.Posts.Where(x => x.Id == selectUser[0].Post).ToList();
                List<Deportament> deportaments = context.Deportaments.Where(x => x.Id == posts[0].DeportamentId).ToList();
                UserDatum userCurrent = selectUser.FirstOrDefault();
                Deportament deportamentCurrent = deportaments.FirstOrDefault();
                Post postCurrent = posts.FirstOrDefault();
                if (userCurrent != null)
                {
                    MainWindowViewModel.main.CurrentControl = new PinCodeVM();
                    MainWindowViewModel.main.CurrentUser = userCurrent;
                    MainWindowViewModel.main.Posts = postCurrent;
                    MainWindowViewModel.main.Deport = deportamentCurrent;

                }
                else
                {
                    ErrorMessage = "*Invalid login or password";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "*No connection";
            }

        }
        #endregion

        #endregion

        /*------------------------------------------- Command ---------------------------------------------------*/
        #endregion


        #region Property
        /* ---------------------------------------- Property ---------------------------------------- */

        #region Property

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

    }
}
