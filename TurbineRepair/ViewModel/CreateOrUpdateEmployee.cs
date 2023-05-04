using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateEmployee : Base.ViewModel
    {

        #region Prop

        #region SelectPost
        private Post _selectPost;
        public Post SelectPost
        {
            get=> _selectPost; 
            set => Set(ref _selectPost, value);
        }
        #endregion

        #region SelectRole
        private Role _selectRole;
        public Role SelectRole
        {
            get => _selectRole;
            set => Set(ref _selectRole, value);
        }
        #endregion

        #region ImagePath
        private string? _imagePath;
        public string? ImagePath
        {
            get => _imagePath;
            set => Set(ref _imagePath, value);
        }
        #endregion

        #endregion


        #region List

        #region PostList
        private List<Post> _postsList = MainWindowViewModel.main.PostsAll.ToList();
        public List<Post> PostsList
        {
            get => _postsList;
            set => Set(ref _postsList, value);
        }
        #endregion

        #region RoleList
        private List<Role> _roleList = MainWindowViewModel.main.RoleAll.ToList();
        public List<Role> RoleList
        {
            get => _roleList;
            set => Set(ref _roleList, value);
        }
        #endregion

        #endregion

        public CreateOrUpdateEmployee()
        {


        }
    }
}
