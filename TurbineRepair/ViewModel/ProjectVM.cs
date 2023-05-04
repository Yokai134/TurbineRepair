using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Migration;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class ProjectVM : Base.ViewModel
    {
        public static ProjectVM projectVM;


        public ICommand SearchUserProject { get; }

        private bool CanSearchUserProjectExecute(object parametr) => true;

        public void OnSearchUserProjectExecute(object parametr)
        {
            try {

                if (SelectUser != null)
                {
                    ProjectItem = Projects.Where(x => x.ProjectExecutorNavigation.Surname.StartsWith(SelectUser)).ToList();
                }
                else if(SelectUser == "")ProjectItem = Projects;
            }
            catch
            {
                MessageBox.Show("Исполнитель не найден", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
            }


        }


        #region ProjectItem
        private object _projectItem;
        public object ProjectItem
        {
            get => _projectItem;
            set => Set(ref _projectItem, value);
        }
        #endregion

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

        public ProjectVM()
        {
            if (MainWindowViewModel.main.CurrentUser.Role != 1)
            {
                Projects = MainWindowViewModel.main.ProjectData.Where(x => x.ProjectExecutor == MainWindowViewModel.main.CurrentUser.Id).ToList();
            }
            else if(MainWindowViewModel.main.CurrentUser.Role == 1)
            {
                Projects = MainWindowViewModel.main.ProjectData;
            }

            Users = MainWindowViewModel.main.UsersAll;
          

            ProjectItem = Projects;

          
            projectVM = this;

            SearchUserProject = new LambdaCommand(OnSearchUserProjectExecute, CanSearchUserProjectExecute);
            
        }
    }
}
