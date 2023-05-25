using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Migration;
using TurbineRepair.Model;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class ProjectVM : Base.ViewModel
    {
        public static ProjectVM projectVM;

        #region Command

        #region SearchProject
        public ICommand SearchUserProject { get; }

        private bool CanSearchUserProjectExecute(object parametr) => true;

        public void OnSearchUserProjectExecute(object parametr)
        {

            if (MainWindowViewModel.main.CurrentUser.Role != 1)
            {
                if (SelectUser != null)
                {
                    ProjectItem = Projects.Where(x => x.ProjectName.StartsWith(SelectUser)).ToList();
                }
                else ProjectItem = Projects;
            }
            else
            {
                if (SelectUser != null)
                {
                    ProjectItem = Projects.Where(x => x.ProjectExecutorNavigation.Surname.StartsWith(SelectUser)).ToList();
                }
                else
                {
                    ProjectItem = Projects;
                }
            
            }
                                 

        }
        #endregion

        #region CreateProject

        public ICommand CreateProject { get; }

        private bool CanCreateProjectExecute(object parametr) => true;
        private void OnCreateProjectExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateProjectVM();
        }

        #endregion

        #region UpdateProject
        public ICommand UpdateProject { get; }
        private bool CanUpdateProjectExecute(object parametr) => true;
        private void OnUpdateProjectExecute(object parametr)
        {
            if(SelectedProject != null)
            {
                MainWindowViewModel.main.UpdProject = SelectedProject;
                MainVM.mainVM.MainCurrentControl = new CreateOrUpdateProjectVM();
            }
            else
            {
                ForegroundFailedMessage = -1;
                FailedAddOrUpdateContent = "*Не выбран проект";
            }

        }

        #endregion

        #region DeleteProject
        public ICommand DeleteProject { get; }
        private bool CanDeleteProjectExecute(object parametr) => true;
        private async void OnDeleteProjectExecute(object parametr)
        {
           
            if (SelectedProject != null)
            {
                ProjectDatum delProject = SelectedProject;
                delProject.DeleteProject = true;
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                ProjectItem = MainWindowViewModel.main.ProjectData.Where(x=>x.DeleteProject == false).ToList();
                ForegroundFailedMessage = 1;
                FailedAddOrUpdateContent = "*Проект удален";
            }
            else
            {
                ForegroundFailedMessage = -1;
                FailedAddOrUpdateContent = "*Не выбран проект";
            }
               

        }

        #endregion


        #endregion

        #region Property
        private object _projectItem;
        public object ProjectItem
        {
            get => _projectItem;
            set => Set(ref _projectItem, value);
        }
        

        private string _selectUser;
        public string SelectUser
        {
            get => _selectUser;
            set => Set(ref _selectUser, value);
        }

        private ProjectDatum _selectedProject;
        public ProjectDatum SelectedProject
        {
            get => _selectedProject;
            set => Set(ref _selectedProject, value);
        }

        public List<ProjectDatum> Projects { get; set; }

        public List<UserDatum> Users { get; set; }

        public UserDatum CurrentUser = MainWindowViewModel.main.CurrentUser;

        private bool _checkRole;
        public bool CheckRole
        {
            get => _checkRole;
            set => Set(ref _checkRole, value);
        }

        private string _contentButton;
        public string ContentButton
        {
            get => _contentButton;
            set => Set(ref _contentButton, value);
        }

        private string _failedAddOrUpdateContent;
        public string FailedAddOrUpdateContent
        {
            get => _failedAddOrUpdateContent;
            set => Set(ref _failedAddOrUpdateContent, value);
        }

        private decimal _foregroundFailedMessage;
        public decimal ForegroundFailedMessage
        {
            get => _foregroundFailedMessage;
            set => Set(ref _foregroundFailedMessage, value);
        }

        #endregion

        public ProjectVM()
        {
            if (MainWindowViewModel.main.CurrentUser.Role != 1)
            {
                Projects = MainWindowViewModel.main.ProjectData.Where(x => x.ProjectExecutor == MainWindowViewModel.main.CurrentUser.Id && x.DeleteProject == false).ToList();
                CheckRole = false;
            }
            else if(MainWindowViewModel.main.CurrentUser.Role == 1)
            {
                Projects = MainWindowViewModel.main.ProjectData.Where(x=>x.DeleteProject == false).ToList();
                CheckRole = true;
            }

            Users = MainWindowViewModel.main.UsersAll;
          

            ProjectItem = Projects;

          
            projectVM = this;

            SearchUserProject = new LambdaCommand(OnSearchUserProjectExecute, CanSearchUserProjectExecute);

            CreateProject = new LambdaCommand(OnCreateProjectExecute, CanCreateProjectExecute);

            UpdateProject = new LambdaCommand(OnUpdateProjectExecute, CanUpdateProjectExecute);

            DeleteProject = new LambdaCommand(OnDeleteProjectExecute, CanDeleteProjectExecute);
            
        }
    }
}
