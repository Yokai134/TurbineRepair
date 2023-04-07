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

        #region CloseAppCommadn
        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecute(object parameter) => true;

        private void OnCloseAppCommandExecution(object parameter)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        public PinCodeVM() 
        {
            #region Command

            #region CloseAppCommand
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecution, CanCloseAppCommandExecute);
            #endregion

            #endregion
        }
    }
}
