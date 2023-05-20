using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls;
using TurbineRepair.ViewModel;
using TurbineRepair.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public static MainWindowViewModel main;
        public static TurbinerepairContext context = new TurbinerepairContext();


        /* ------------------------------------------ UserControl ----------------------------------*/

        #region UserControl
        private AutheticationVM? autheticationVM { get; set; }
        #endregion

        /* ------------------------------------------ UserControl ----------------------------------*/



        /* ------------------------------------------- Property ------------------------------------*/

        #region Property

        #region CurrentControl
        private object? _currentControl;

        public object? CurrentControl
        {
            get => _currentControl;
            set => Set(ref _currentControl, value);
        }
        #endregion

        #region CurrentUser
        private UserDatum? _currentUser;
        public UserDatum? CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
        #endregion

        #region CurrentDeport
        private Deportament? _deport;
        public Deportament? Deport
        {
            get => _deport;
            set => Set(ref _deport, value);
        }
        #endregion

        #region CurrentPost
        private Post? _posts;
        public Post? Posts
        {
            get => _posts;
            set => Set(ref _posts, value);
        }
        #endregion

        #region CurrentProject
        private ProjectDatum? _currentProject = null;
        public ProjectDatum? CurrentProject
        {
            get => _currentProject;
            set => Set(ref _currentProject, value);
        }

        #endregion

        #region UpdateDataUser
        private UserDatum? _updateUser;
        public UserDatum? UpdateUser
        {
            get => _updateUser;
            set => Set(ref _updateUser, value);
        }
        #endregion

        #region UpdateProject
        private ProjectDatum? _updProject;
        public ProjectDatum? UpdProject
        {
            get => _updProject;
            set => Set(ref _updProject, value);
        }
        #endregion

        #region UpdateTurbine
        private Turbine? _updTurbine;
        public Turbine? UpdTurbine
        {
            get => _updTurbine;
            set => Set(ref _updTurbine, value);
        }

        private Turbine? _createTurbine;
        public Turbine? CreateTurbine
        {
            get => _createTurbine;
            set => Set(ref _createTurbine, value);
        }

        private Customer? _updCustomer;
        public  Customer? UpdCustomer
        {
            get => _updCustomer;
            set => Set(ref _updCustomer, value);
        }

        private Oraganization? _updOraganization;
        public Oraganization? UpdOraganization
        {
            get => _updOraganization;
            set => Set(ref _updOraganization, value);
        }
        #endregion

        #endregion

        /* ------------------------------------------- Property ------------------------------------*/



        /* --------------------------------------------- List --------------------------------------*/

        #region List

        #region ProjectData
        private List<ProjectDatum> _projectData = context.ProjectData.ToList();
        public List<ProjectDatum> ProjectData
        {
            get => _projectData;
            set => Set(ref _projectData, value);
        }
        #endregion

        #region Customers

        private List<Customer> _customersAll = context.Customers.OrderBy(x => x.CustomerSurname).ToList();
        public List<Customer> CustomersAll
        {
            get=> _customersAll;
            set=> Set(ref _customersAll, value);
        }

        #endregion

        #region Turbine

        private List<Turbine> _turbinesAll = context.Turbines.ToList();

        public List<Turbine> TurbinesAll
        {
            get => _turbinesAll;
            set => Set(ref _turbinesAll, value);
        }
        #endregion

        #region Status
        private List<StatusProject> _statusesAll = context.StatusProjects.ToList();
        public List<StatusProject> StatusesAll
        {
            get => _statusesAll;
            set => Set(ref _statusesAll, value);
        }
        #endregion

        #region UserAll
        private List<UserDatum> _usersAll = context.UserData.OrderBy(x=>x.Surname).ToList();
        public List<UserDatum> UsersAll
        {
            get => _usersAll;
            set => Set(ref _usersAll, value);
        }
        #endregion

        #region TurbineData

        private List<TurbineScpg> _turbineScpg = context.TurbineScpgs.ToList();
        public List<TurbineScpg> TurbineScpgs
        {
            get => _turbineScpg;
            set => Set(ref _turbineScpg, value);
        }

        private List<TurbineMdum> _turbineMda = context.TurbineMda.ToList();
        public List<TurbineMdum> TurbineMda
        {
            get => _turbineMda;
            set => Set(ref _turbineMda, value);
        }


        private List<TurbinePgp> _turbinePgp = context.TurbinePgps.ToList();
        public List<TurbinePgp> TurbinePgp
        {
            get => _turbinePgp;
            set => Set(ref _turbinePgp, value);
        }

        private List<TurbineMdp> _turbineMdp = context.TurbineMdps.ToList();
        public List<TurbineMdp> TurbineMdp
        {
            get => _turbineMdp;
            set => Set(ref _turbineMdp, value);
        }

        private List<TurbineImage> _turbineImage = context.TurbineImages.ToList();
        public List<TurbineImage> TurbineImage
        {
            get => _turbineImage;
            set => Set(ref _turbineImage, value);
        }

        #endregion

        #region Posts
        private List<Post> _postsAll = context.Posts.ToList();
        public List<Post> PostsAll
        {
            get => _postsAll;
            set => Set(ref _postsAll, value);
        }
        #endregion

        #region Deportaments

        private List<Deportament> _deportamentsAll = context.Deportaments.ToList();
        public List <Deportament> DeportamentsAll
        {
            get => _deportamentsAll;
            set => Set(ref _deportamentsAll, value);
        }
        #endregion

        #region Role
        private List<Role> _roleAll = context.Roles.ToList();
        public List<Role> RoleAll
        {
            get => _roleAll;
            set => Set(ref _roleAll, value);
        }
        #endregion

        #region Organization
        private List<Oraganization> _oraganizations = context.Oraganizations.ToList();

        public List<Oraganization> Oraganizations
        {
            get => _oraganizations;
            set => Set(ref _oraganizations, value);
        }
        #endregion

        #region TypeOfWork
        private List<TypeOfWork> _typesWork = context.TypeOfWorks.ToList();
        public List<TypeOfWork> TypesWork
        {
            get => _typesWork;
            set => Set(ref _typesWork, value);
        }
        #endregion

        #endregion

        /* --------------------------------------------- List --------------------------------------*/


        /// <summary>
        /// Логика взаимодействия с MainWindow.xaml
        /// </summary>
        public MainWindowViewModel() 
        {           
            CurrentControl = new AutheticationVM();
            main = this;
        }


        #region UpdateDate
        public async Task UpdateData()
        {
            await Task.Run(() => ProjectData = context.ProjectData.ToList());
            await Task.Run(() => CustomersAll = context.Customers.OrderBy(x => x.CustomerSurname).ToList());
            await Task.Run(() => TurbinesAll = context.Turbines.ToList());
            await Task.Run(() => StatusesAll = context.StatusProjects.ToList());
            await Task.Run(() => UsersAll = context.UserData.OrderBy(x => x.Surname).ToList());
            await Task.Run(() => TurbineImage = context.TurbineImages.ToList());
            await Task.Run(() => TurbineScpgs = context.TurbineScpgs.ToList());
            await Task.Run(() => TurbineMda = context.TurbineMda.ToList());
            await Task.Run(() => TurbinePgp = context.TurbinePgps.ToList());
            await Task.Run(() => TurbineMdp = context.TurbineMdps.ToList());
            await Task.Run(() => Oraganizations = context.Oraganizations.ToList());            
        }
        #endregion


    }
}
