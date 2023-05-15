﻿using ScottPlot.Renderable;
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
            try
            {
                if(SelectedProject != null)
                {
                    MainWindowViewModel.main.UpdProject = SelectedProject;
                    MainVM.mainVM.MainCurrentControl = new CreateOrUpdateProjectVM();
                }
                else MessageBox.Show("Не выбран проект", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Не выбран проект", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region DeleteProject
        public ICommand DeleteProject { get; }
        private bool CanDeleteProjectExecute(object parametr) => true;
        private async void OnDeleteProjectExecute(object parametr)
        {
            try
            {
                if (SelectedProject != null)
                {
                    ProjectDatum delProject = SelectedProject;
                    delProject.DeleteProject = true;
                    await MainWindowViewModel.main.UpdateData();
                    ProjectItem = MainWindowViewModel.main.ProjectData.Where(x=>x.DeleteProject == false).ToList();
                }
                else MessageBox.Show("Проект удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Не выбран проект", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #endregion

        public ProjectVM()
        {
            if (MainWindowViewModel.main.CurrentUser.Role != 1)
            {
                Projects = MainWindowViewModel.main.ProjectData.Where(x => x.ProjectExecutor == MainWindowViewModel.main.CurrentUser.Id).ToList();
                CheckRole = false;
            }
            else if(MainWindowViewModel.main.CurrentUser.Role == 1)
            {
                Projects = MainWindowViewModel.main.ProjectData;
                CheckRole = true;
            }

            Users = MainWindowViewModel.main.UsersAll;
          

            ProjectItem = Projects;

          
            projectVM = this;

            SearchUserProject = new LambdaCommand(OnSearchUserProjectExecute, CanSearchUserProjectExecute);

            CreateProject = new LambdaCommand(OnCreateProjectExecute, CanCreateProjectExecute);

            UpdateProject = new LambdaCommand(OnUpdateProjectExecute, CanUpdateProjectExecute);
            
        }
    }
}
