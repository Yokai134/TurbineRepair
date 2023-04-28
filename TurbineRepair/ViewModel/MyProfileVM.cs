using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class MyProfileVM : Base.ViewModel
    {
        #region Property
        /*---------------------------------- Property -------------------------------------*/

        #region NameUser
        private string _name;
        public string Name
        {
            get => _name;
            set=> Set(ref _name, value);
        }
        #endregion
        /*---------------------------------- Property -------------------------------------*/
        #endregion
        public MyProfileVM() 
        {

        }


    }
}
