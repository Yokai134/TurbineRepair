using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateProjectVM : Base.ViewModel, INotifyDataErrorInfo
    {
        string decimalNumber = @"^\d+?$";

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

        private string _contentButton;
        public string ContentButton
        {
            get => _contentButton;
            set => Set(ref _contentButton, value);
        }

        #region ProjectField

        private UserDatum _selectFirstExecutor;
        public UserDatum SelectFirstExecutor
        {
            get => _selectFirstExecutor;
            set
            {
                Set(ref _selectFirstExecutor, value);
                ValidateFirstExecutor();
            }
        }

        private UserDatum _selectSecondExecutor;
        public UserDatum SelectSecondExecutor
        {
            get => _selectSecondExecutor;
            set
            {
                Set(ref _selectSecondExecutor, value);
                ValidateSecondExecutor();
            }
        }

        private string _dateStart = DateTime.Now.ToShortDateString();
        public string DateStart
        {
            get => _dateStart;
            set
            {
                Set(ref _dateStart, value);
                ValidateDateStart();
                ValidateDateEnd();
            }
        }

        private string _dateEnd = DateTime.Now.ToShortDateString();
        public string DateEnd
        {
            get => _dateEnd;
            set
            {
                Set(ref _dateEnd, value);
                ValidateDateEnd();
                ValidateDateStart();
            }
        }

        private Customer _selectCustomer;
        public Customer SelectCustomer
        {
            get => _selectCustomer;
            set
            {
                Set(ref _selectCustomer, value);
                ValidateCustomer();
            }
        }

        private Turbine _selectTurbine;
        public Turbine SelectTurbine
        {
            get => _selectTurbine;
            set
            {
                Set(ref _selectTurbine, value);
                ValidateTurbine();
            }
        }

        private StatusProject _selectStatusProject;
        public StatusProject SelectStatusProject
        {
            get => _selectStatusProject;
            set
            {
                Set(ref _selectStatusProject, value);
                ValidateStatus();
            }
        }

        private string _selectCount;
        public string SelectCount
        {
            get => _selectCount;
            set
            {
                Set(ref _selectCount, value);
                ValidateCount();
            }
        }

        #endregion

        #region BoolValidation
        private bool _checkFirstExecutor;
        public bool CheckFirstExecutor
        {
            get => _checkFirstExecutor;
            set => Set(ref _checkFirstExecutor, value);
        }
        private bool _checkSecondExecutor;
        public bool CheckSecondExecutor
        {
            get => _checkSecondExecutor;
            set => Set(ref _checkSecondExecutor, value);
        }
        private bool _checkStatus;
        public bool CheckStatus
        {
            get => _checkStatus;
            set => Set(ref _checkStatus, value);
        }
        private bool _checkDateStart;
        public bool CheckDateStart
        {
            get => _checkDateStart;
            set => Set(ref _checkDateStart, value);
        }
        private bool _checkDateEnd;
        public bool CheckDateEnd
        {
            get => _checkDateEnd;
            set => Set(ref _checkDateEnd, value);
        }
        private bool _checkTurbine;
        public bool CheckTurbine
        {
            get => _checkTurbine;
            set => Set(ref _checkTurbine, value);
        }
        private bool _checkCustomer;
        public bool CheckCustomer
        {
            get => _checkCustomer;
            set => Set(ref _checkCustomer, value);
        }
        private bool _checkCount;
        public bool CheckCount
        {
            get => _checkCount;
            set => Set(ref _checkCount, value);
        }
        #endregion

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

        #region Command

        public ICommand CreateOrUpdateProject { get; }

        private bool CanCreateOrUpdateProjectExecute(object parametr) => true;
        private async void OnCreateOrUpdateProjectExecute(object parametr)
        {
            if (MainWindowViewModel.main.UpdProject != null)
            {
                #region ValidationPreUpdate
                ValidateFirstExecutor();
                ValidateSecondExecutor();
                ValidateCustomer();
                ValidateCount();
                ValidateDateStart();
                ValidateDateEnd();
                ValidateStatus();
                ValidateTurbine();
                #endregion
                if (CheckFirstExecutor && CheckSecondExecutor && CheckDateStart && CheckDateEnd && CheckCount && CheckStatus && CheckCustomer && CheckTurbine)
                {
                    ProjectDatum updProject = MainWindowViewModel.main.UpdProject;
                    updProject.ProjectName = "Заказ на изготовление турбины " + SelectTurbine.TurbineName;
                    updProject.ProjectExecutor = SelectFirstExecutor.Id;
                    updProject.ProjectSecondExecutor = SelectSecondExecutor.Id;
                    updProject.ProjectCustomer = SelectCustomer.Id;
                    updProject.ProjectDataStart = DateOnly.FromDateTime(Convert.ToDateTime(DateStart));
                    updProject.ProjectDataEnd = DateOnly.FromDateTime(Convert.ToDateTime(DateEnd));
                    updProject.DeleteProject = false;
                    updProject.ProjectStatus = SelectStatusProject.Id;
                    updProject.TypeProject = 1;
                    updProject.ProjectTurbine = SelectTurbine.Id;
                    updProject.ProjectCount = Convert.ToInt32(SelectCount);
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdateContent = "Данные проекта обновлены";
                    ForegroundFailedMessage = 1;

                }
                else
                {
                    FailedAddOrUpdateContent = "Не удалось обновить данные, провертe правильность ввода";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
            else
            {
                #region ValidationPreCreate
                ValidateFirstExecutor();
                ValidateSecondExecutor();
                ValidateCustomer();
                ValidateCount();
                ValidateDateStart();
                ValidateDateEnd();
                ValidateStatus();
                ValidateTurbine();
                #endregion
                if (CheckFirstExecutor && CheckSecondExecutor && CheckDateStart && CheckDateEnd && CheckCount && CheckStatus && CheckCustomer && CheckTurbine)
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
                        ProjectCount = Convert.ToInt32(SelectCount)
                    };
                    MainWindowViewModel.context.ProjectData.Add(newProject);
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdateContent = "Проект создан";
                    ForegroundFailedMessage = 1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshContent;
                    timer.Start();

                }
                else
                {
                    FailedAddOrUpdateContent = "Не удалось создать, провертe правильность ввода";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
            
        }
        public ICommand BackProjectList { get; }
        private bool CanBackProjectListExecute(object parametr) => true;
        private void OnBackProjectListExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new ProjectVM();
        }
        #endregion


        public CreateOrUpdateProjectVM()
        {
         
            FirstExercutor = MainWindowViewModel.main.UsersAll.Where(x=>x.Role == 2 && x.DeleteUser == false).ToList();
            SecondExercutor = MainWindowViewModel.main.UsersAll.Where(x=>x.Role == 2 && x.DeleteUser == false).ToList();
            StatusProjects = MainWindowViewModel.main.StatusesAll.Where(x=>x.Id == 4).ToList();
            Customers = MainWindowViewModel.main.CustomersAll.Where(x=>x.DeleteCustomer == false).ToList();
            Turbines = MainWindowViewModel.main.TurbinesAll.Where(x=>x.DeleteTurbine == false).ToList();
            

            if(MainWindowViewModel.main.UpdProject != null)
            {
                if (MainWindowViewModel.main.CurrentUser.Role != 1)
                {
                    FirstExercutor = MainWindowViewModel.main.UsersAll.Where(x => x.Id == MainWindowViewModel.main.CurrentUser.Id).ToList();
                }
                else
                {
                    FirstExercutor = MainWindowViewModel.main.UsersAll.Where(x => x.Role == 2 && x.DeleteUser == false).ToList();
                }
                StatusProjects = MainWindowViewModel.main.StatusesAll.ToList();
                SelectFirstExecutor = MainWindowViewModel.main.UsersAll.Where(x => x.Id == MainWindowViewModel.main.UpdProject.ProjectExecutorNavigation.Id).FirstOrDefault();
                SelectSecondExecutor = MainWindowViewModel.main.UsersAll.Where(x => x.Id == MainWindowViewModel.main.UpdProject.ProjectSecondExecutorNavigation.Id).FirstOrDefault();
                DateStart = MainWindowViewModel.main.UpdProject.ProjectDataStart.ToString();
                DateEnd = MainWindowViewModel.main.UpdProject.ProjectDataEnd.ToString();
                SelectCustomer = MainWindowViewModel.main.CustomersAll.Where(x => x.Id == MainWindowViewModel.main.UpdProject.ProjectCustomerNavigation.Id).FirstOrDefault();
                SelectTurbine = MainWindowViewModel.main.TurbinesAll.Where(x => x.Id == MainWindowViewModel.main.UpdProject.ProjectTurbineNavigation.Id).FirstOrDefault();
                SelectStatusProject = MainWindowViewModel.main.StatusesAll.Where(x => x.Id == MainWindowViewModel.main.UpdProject.ProjectStatusNavigation.Id).FirstOrDefault();
                SelectCount = MainWindowViewModel.main.UpdProject.ProjectCount.ToString();
                ContentButton = "Изменить";
            }
            else
            {
                if (MainWindowViewModel.main.CurrentUser.Role != 1)
                {
                    FirstExercutor = MainWindowViewModel.main.UsersAll.Where(x => x.Id == MainWindowViewModel.main.CurrentUser.Id).ToList();
                }
                else
                {
                    FirstExercutor = MainWindowViewModel.main.UsersAll.Where(x => x.Role == 2 && x.DeleteUser == false).ToList();
                }
                ContentButton = "Добавить";
            }

            CreateOrUpdateProject = new LambdaCommand(OnCreateOrUpdateProjectExecute, CanCreateOrUpdateProjectExecute);
            BackProjectList = new LambdaCommand(OnBackProjectListExecute, CanBackProjectListExecute);
        }


        #region Validation

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }


        private void ValidateFirstExecutor()
        {
            ClearErrors(nameof(SelectFirstExecutor));
            if (SelectFirstExecutor == null)
            {
                ClearErrors(nameof(SelectFirstExecutor));
                AddError(nameof(SelectFirstExecutor), "*Не выбран главный ответственный.");
                CheckFirstExecutor = false;
            }
            else if (SelectFirstExecutor == SelectSecondExecutor)
            {
                ClearErrors(nameof(SelectFirstExecutor));
                AddError(nameof(SelectFirstExecutor), "*Главный ответсвенный и ответственные не могут быть одним лицом.");
                CheckFirstExecutor = false;
            }
            else
            {
                CheckFirstExecutor = true;
            }

        }

        private void ValidateSecondExecutor()
        {
            ClearErrors(nameof(SelectSecondExecutor));
            if (SelectSecondExecutor == null)
            {
                ClearErrors(nameof(SelectSecondExecutor));
                AddError(nameof(SelectSecondExecutor), "*Не выбран ответственный , главный ответсвенный и ответственные не могут быть одним лицом.");
                CheckSecondExecutor = false;
            }
            else if (SelectSecondExecutor == SelectFirstExecutor)
            {
                ClearErrors(nameof(SelectSecondExecutor));
                AddError(nameof(SelectSecondExecutor), "*Главный ответсвенный и ответственные не могут быть одним лицом.");
                CheckFirstExecutor = false;
            }
            else
            {
                CheckSecondExecutor = true;
            }

        }

        private void ValidateStatus()
        {
            ClearErrors(nameof(SelectStatusProject));
            if (SelectStatusProject == null)
            {
                ClearErrors(nameof(SelectStatusProject));
                AddError(nameof(SelectStatusProject), "*Укажите статус проекта.");
                CheckStatus = false;
            }
            else
            {
                CheckStatus = true;
            }

        }

        private void ValidateDateStart()
        {
            ClearErrors(nameof(DateStart));
            if (string.IsNullOrWhiteSpace(DateStart))
            {
                ClearErrors(nameof(DateStart));
                AddError(nameof(DateStart), "*Не выбрана дата.");
                CheckDateStart = false;

            }
            else if (Convert.ToDateTime(DateStart) >= Convert.ToDateTime(DateEnd))
            {
                ClearErrors(nameof(DateStart));
                AddError(nameof(DateStart), "*Дата начала не может превышать дату окончания.");
                CheckDateStart = false;
            }
            else
            {
                CheckDateStart = true;
            }
            

        }

        private void ValidateDateEnd()
        {
            ClearErrors(nameof(DateEnd));
            if (string.IsNullOrWhiteSpace(DateEnd))
            {
                ClearErrors(nameof(DateStart));
                AddError(nameof(DateStart), "*Не выбрана дата.");
                CheckDateEnd = false;

            }
            else if (Convert.ToDateTime(DateEnd) <= Convert.ToDateTime(DateStart))
            {
                ClearErrors(nameof(DateEnd));
                AddError(nameof(DateEnd), "*Дата окончания не может быть меньше даты начала.");
                CheckDateEnd = false;
            }
            else
            {
                CheckDateEnd = true;
            }

        }
       
        private void ValidateTurbine()
        {
            ClearErrors(nameof(SelectTurbine));
            if(SelectTurbine == null)
            {
                ClearErrors(nameof(SelectTurbine));
                AddError(nameof(SelectTurbine), "*Не выбрана производимая турбина.");
                CheckTurbine = false;
            }
            else
            {
                CheckTurbine = true;
            }
        }

        private void ValidateCustomer()
        {
            ClearErrors(nameof(SelectCustomer));
            if (SelectCustomer == null)
            {
                ClearErrors(nameof(SelectCustomer));
                AddError(nameof(SelectCustomer), "*Не выбрана заказчик.");
                CheckCustomer = false;
            }
            else
            {
                CheckCustomer = true;
            }
        }

        private void ValidateCount()
        {
            ClearErrors(nameof(SelectCount));
            if (string.IsNullOrWhiteSpace(SelectCount) || !Regex.IsMatch(SelectCount, decimalNumber, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(SelectCount));
                AddError(nameof(SelectCount), "*Поле не может быть пустым, введите число.");
                CheckCount = false;
            }
            else
            {
                CheckCount = true;
            }

        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        #endregion

        #region DispatherTimer
        DispatcherTimer timer = new DispatcherTimer();
        private void RefreshValidator(object e, object sender)
        {
            ForegroundFailedMessage = 0;
            FailedAddOrUpdateContent = "";
            ClearErrors(nameof(SelectFirstExecutor));
            ClearErrors(nameof(SelectSecondExecutor));
            ClearErrors(nameof(SelectCount));
            ClearErrors(nameof(SelectCustomer));
            ClearErrors(nameof(SelectStatusProject));
            ClearErrors(nameof(SelectTurbine));
            ClearErrors(nameof(DateStart));
            ClearErrors(nameof(DateEnd));
            timer.Stop();
        }
        private void RefreshContent(object e, object sender)
        {
            CheckFirstExecutor = false;
            CheckSecondExecutor = false;
            CheckDateStart = false;
            CheckDateEnd = false;
            CheckCount = false;
            CheckStatus = false;
            CheckCustomer = false;
            CheckTurbine = false;
            SelectFirstExecutor = null;
            SelectSecondExecutor = null;
            SelectCustomer = null;
            DateStart = DateTime.Now.ToShortDateString();
            DateEnd = DateTime.Now.ToShortDateString();
            SelectCount = string.Empty;
            SelectStatusProject = null;
            SelectTurbine = null;
            FailedAddOrUpdateContent = string.Empty;
            timer.Stop();
        }
        private void OpenProjectList(object sender, object e)
        {
            MainVM.mainVM.MainCurrentControl = new ProjectVM();
            timer.Stop();
        }
        #endregion
    }
}
