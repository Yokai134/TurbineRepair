﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateProjectVM : Base.ViewModel
    {

        #region Property

        private List<UserDatum> _firstExecutor;
        public List<UserDatum> FirstExercutor
        {
            get => _firstExecutor;
            set => Set(ref _firstExecutor, value);
        }

        private List<UserDatum> _secondExecutor;
        public List<UserDatum> SecondExercutor
        {
            get => _secondExecutor;
            set => Set(ref _secondExecutor, value);
        }

        private List<StatusProject> _statusProject;
        public List<StatusProject> StatusProjects
        {
            get => _statusProject;
            set => Set(ref _statusProject, value);
        }

        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get => _customers;
            set => Set(ref _customers, value);
        }

        private List<Turbine> _turbines;
        public List<Turbine> Turbines
        {
            get => _turbines;
            set => Set(ref _turbines, value);
        }
        #endregion

        #region ProjectField

        private UserDatum _selectFirstExecutor;
        public UserDatum SelectFirstExecutor
        {
            get => _selectFirstExecutor;
            set => Set(ref _selectFirstExecutor, value);
        }

        private UserDatum _selectSecondExecutor;
        public UserDatum SelectSecondExecutor
        {
            get => _selectSecondExecutor;
            set => Set(ref _selectSecondExecutor, value);
        }

        private string _dateStart = DateTime.Now.ToString();
        public string DateStart
        {
            get => _dateStart;
            set => Set(ref _dateStart, value);
        }

        private string _dateEnd = DateTime.Now.ToString();
        public string DateEnd
        {
            get => _dateEnd;
            set => Set(ref _dateEnd, value); 
        }

        private Customer _selectCustomer;
        public Customer SelectCustomer
        {
            get => _selectCustomer; 
            set => Set(ref _selectCustomer, value);
        }

        private Turbine _selectTurbine;
        public Turbine SelectTurbine
        {
            get => _selectTurbine;
            set => Set(ref _selectTurbine, value);
        }

        private StatusProject _selectStatusProject;
        public StatusProject SelectStatusProject
        {
            get => _selectStatusProject;
            set => Set(ref _selectStatusProject, value);
        }

        private string _selectCost;
        public string SelectCost
        {
            get => _selectCost;
            set => Set(ref _selectCost, value);
        }

        #endregion

        #region Command

        public ICommand CreateOrUpdateProject { get; }

        private bool CanCreateOrUpdateProjectExecute(object parametr) => true;
        private async void OnCreateOrUpdateProjectExecute(object parametr)
        {
            try {
                if (MainWindowViewModel.main.UpdProject != null)
                {

                }
                else
                {
                    if (SelectFirstExecutor != null && SelectSecondExecutor != null && SelectCustomer != null && DateEnd != null && DateStart != null && Convert.ToDateTime(DateEnd) > Convert.ToDateTime(DateStart)
                        && SelectStatusProject != null
                        && SelectTurbine != null && SelectCost != null)
                    {
                        ProjectDatum newProject = new ProjectDatum()
                        {
                            ProjectName = "Заказ на изготовление турбины " + SelectTurbine.TurbineName,
                            ProjectExecutor = SelectFirstExecutor.Id,
                            ProjectSecondExecutor = SelectSecondExecutor.Id,
                            ProjectCustomer = SelectCustomer.Id,
                            ProjectDataStart = DateOnly.FromDateTime(Convert.ToDateTime(DateStart)),
                            ProjectDataEnd = DateOnly.FromDateTime(Convert.ToDateTime(DateEnd)),
                            DeleteProject = false,
                            ProjectStatus = SelectStatusProject.Id,
                            TypeProject = 1,
                            ProjectTurbine = SelectTurbine.Id,
                            ProjectCost = Convert.ToDecimal(SelectCost)
                        };
                        MainWindowViewModel.context.ProjectData.Add(newProject);
                        MainWindowViewModel.context.SaveChanges();
                        await MainWindowViewModel.main.UpdateData();
                        MessageBox.Show("Проект успешно создан", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    else
                    {
                        MessageBox.Show("Не выбран(-ы) ответсвенный(-ые), не указан заказачик, статус проекта, производимая турбина, цена, или дата окончания меньше даты начала", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                }
            }
            catch
            {
                MessageBox.Show("ЫЧА", "ЫЧА");
            }
        }
        #endregion


        public CreateOrUpdateProjectVM()
        {
            FirstExercutor = MainWindowViewModel.main.UsersAll.Where(x=>x.Role == 2).ToList();
            SecondExercutor = MainWindowViewModel.main.UsersAll.Where(x=>x.Role == 2).ToList();
            StatusProjects = MainWindowViewModel.main.StatusesAll;
            Customers = MainWindowViewModel.main.CustomersAll;
            Turbines = MainWindowViewModel.main.TurbinesAll;

            CreateOrUpdateProject = new LambdaCommand(OnCreateOrUpdateProjectExecute, CanCreateOrUpdateProjectExecute);
        }
    }
}
