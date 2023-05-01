using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TurbineRepair.ViewModel
{
    internal class MyProfileVM : Base.ViewModel
    {

        #region Property
        /*---------------------------------- Property -------------------------------------*/


        /*---------------------------------- Property -------------------------------------*/
        #endregion

        private UserDatum? _userDatum;
        public UserDatum UserDatum
        {
            get => _userDatum;
            set => Set(ref _userDatum, value);
        }

        private Deportament? _deport;
        public Deportament Deport
        {
            get => _deport;
            set => Set(ref _deport, value);
        }

        private Post? _posts;
        public Post Posts
        {
            get => _posts;
            set => Set(ref _posts, value);
        }

        public MyProfileVM() 
        {
            UserDatum = MainWindowViewModel.main.CurrentUser;
            Deport = MainWindowViewModel.main.Deport;
            Posts = MainWindowViewModel.main.Posts;
        }


    }
}
