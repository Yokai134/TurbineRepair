using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;
using TurbineRepair.Infrastructure;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateRequestVM : Base.ViewModel, INotifyDataErrorInfo
    {


        public CreateOrUpdateRequestVM()
        {
            try
            {
                if(MainWindowViewModel.main.CurrentUser.Role == 2)
                {
                    ProjectData = MainWindowViewModel.main.ProjectData.Where(x=>x.ProjectExecutorNavigation.Id == MainWindowViewModel.main.CurrentUser.Id).ToList();
                    ProjectItem = projectData;
                }
                else
                {
                    ProjectData = MainWindowViewModel.main.ProjectData.ToList();
                    ProjectItem = projectData;
                }
            
                TypeOfWorks = MainWindowViewModel.main.TypesWork.Where(x => x.Id != 1).ToList();
                Posts = MainWindowViewModel.main.PostsAll.Where(x => x.DeportamentId == 6).ToList();

                if (MainWindowViewModel.main.UpdRequest != null)
                {
                    StatusProjects = MainWindowViewModel.main.StatusesAll.ToList();
                    RequestName = MainWindowViewModel.main.UpdRequest.TurbineRequestName;
                    RequestDescriptions = MainWindowViewModel.main.UpdRequest.TurbineRequestDescription;
                    SelectProjectRequest = MainWindowViewModel.main.UpdRequest.TurbineRequestProjectNavigation;
                    SelectPost = MainWindowViewModel.main.UpdRequest.TurbineRequestExecutorNavigation;
                    SelectSatus = MainWindowViewModel.main.UpdRequest.TurbineRequestStatusNavigation;
                    SelectTypeOfWork = MainWindowViewModel.main.UpdRequest.TurbineRequestTypeOfWorkNavigation;
                    DateStart = MainWindowViewModel.main.UpdRequest.TurbineRequestDataStart.ToString();
                    DateEnd = MainWindowViewModel.main.UpdRequest.TurbineRequestDataEnd.ToString();
                    ContentButton = "Изменить";
                }
                else
                {
                    StatusProjects = MainWindowViewModel.main.StatusesAll.Where(x => x.StatusName == "Заявка").ToList();
                    ContentButton = "Добавить";
                }

                #region Command
                SearchProject = new LambdaCommand(OnSearchProjectExecute, CanSearchProjectExecutre);
                CreateOrUpdateRequest = new LambdaCommand(OnCreateOrUpdateRequestExecute, CanCreateOrUpdateRequestExecute);
                BackRequest = new LambdaCommand(OnBackRequestExecute, CanBackRequestExecute);
                #endregion
            }
            catch
            {
                ForegroundFailedMessage = -1;
                FiledAddOrUpdateContent = "*Перезагрузите страницу";
            }


        }

        #region Command
        public ICommand SearchProject { get; }
        private bool CanSearchProjectExecutre(object parameter) => true;
        private void OnSearchProjectExecute(object parameter)
        {
            if (ExecutorName != null)
            {
                ProjectItem = ProjectData.Where(x => x.ProjectExecutorNavigation.Surname.StartsWith(ExecutorName)).ToList();
            }
            else ProjectItem = ProjectData;
        }

        public ICommand CreateOrUpdateRequest { get; }
        private bool CanCreateOrUpdateRequestExecute(object parameter) => true;
        private async void OnCreateOrUpdateRequestExecute(object parameter)
        {
            #region Validate
            ValidateTurbineName();
            ValidateDescription();
            ValidateProject();
            ValidatePost();
            ValidateStatus();
            ValidateTypeWork();
            ValidateDateStart();
            ValidateDateEnd();
            #endregion
            if (MainWindowViewModel.main.UpdRequest != null)
            {
                if(_checkName && _checkDescription && _checkProject && _checkPost && _checkStatus && _checkTypeOfWork && _checkDateStart && _checkDateEnd)
                {
                    TurbineRequest updTurbineRequest = MainWindowViewModel.main.UpdRequest;
                    updTurbineRequest.TurbineRequestName = RequestName;
                    updTurbineRequest.TurbineRequestDescription = RequestDescriptions;
                    updTurbineRequest.TurbineRequestProject = SelectProjectRequest.Id;
                    updTurbineRequest.TurbineRequestExecutor = SelectPost.Id;
                    updTurbineRequest.TurbineRequestStatus = SelectSatus.Id;
                    updTurbineRequest.TurbineRequestTypeOfWork = SelectTypeOfWork.Id;
                    updTurbineRequest.TurbineRequestDataStart = DateOnly.FromDateTime(Convert.ToDateTime(DateStart));
                    updTurbineRequest.TurbineRequestDataEnd = DateOnly.FromDateTime(Convert.ToDateTime(DateEnd));
                    await MainWindowViewModel.context.SaveChangesAsync();
                    await MainWindowViewModel.main.UpdateData();
                    ForegroundFailedMessage = 1;
                    FiledAddOrUpdateContent = "*Данные обновлены";

                }
                else
                {
                    FiledAddOrUpdateContent = "Не удалось обновить данные, провертe правильность ввода";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();

                }
            }
            else
            {
                if (_checkName && _checkDescription && _checkProject && _checkPost && _checkStatus && _checkTypeOfWork && _checkDateStart && _checkDateEnd)
                {
                    TurbineRequest newTurbineRequest = new TurbineRequest() {
                        TurbineRequestName = RequestName,
                        TurbineRequestDescription = RequestDescriptions,
                        TurbineRequestProject = SelectProjectRequest.Id,
                        TurbineRequestExecutor = SelectPost.Id,
                        TurbineRequestStatus = SelectSatus.Id,
                        TurbineRequestTypeOfWork = SelectTypeOfWork.Id,
                        TurbineRequestDataStart = DateOnly.FromDateTime(Convert.ToDateTime(DateStart)),
                        TurbineRequestDataEnd = DateOnly.FromDateTime(Convert.ToDateTime(DateEnd))
                    };
                    MainWindowViewModel.context.TurbineRequests.Add(newTurbineRequest);
                    await MainWindowViewModel.context.SaveChangesAsync();
                    await MainWindowViewModel.main.UpdateData();
                    ForegroundFailedMessage = 1;
                    FiledAddOrUpdateContent = "*Заявка созданна";
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshContent;
                    timer.Start();
                }
                else
                {
                    FiledAddOrUpdateContent = "Не удалось добавить данные, провертe правильность ввода";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
        }

        public ICommand BackRequest { get; }
        private bool CanBackRequestExecute(object parameter) => true;
        private void OnBackRequestExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new RequestVM();
        }
        #endregion


      
        #region Property

        private string _filedAddOrUpdateContent;
        public string FiledAddOrUpdateContent
        {
            get => _filedAddOrUpdateContent;
            set => Set(ref _filedAddOrUpdateContent, value);
        }

        private decimal _foregroundFailedMessage;
        public decimal ForegroundFailedMessage
        {
            get => _foregroundFailedMessage;
            set => Set(ref _foregroundFailedMessage, value);
        }
        private string _contentButton;
        public string ContentButton
        {
            get => _contentButton;
            set => Set(ref _contentButton, value);
        }

        private string requestName;
        public string RequestName 
        { 
            get => requestName;
            set
            {
                Set(ref requestName, value);
                ValidateTurbineName();
            }
        }

        private string requestDescriptions;
        public string RequestDescriptions 
        { 
            get => requestDescriptions;
            set
            {
                Set(ref requestDescriptions, value);
                ValidateDescription();
            }

        }

        private string executorName;
        public string ExecutorName { get => executorName; set => Set(ref executorName, value); }

        #region List
        private List<ProjectDatum> projectData;
        public List<ProjectDatum> ProjectData
        {
            get => projectData;
            set => Set(ref projectData, value);
        }
        private List<Post> posts;
        public List<Post> Posts
        {
            get => posts;
            set => Set(ref posts, value);
        }
        private List<StatusProject> statusProjects;
        public List<StatusProject> StatusProjects
        {
            get => statusProjects; set => Set(ref statusProjects, value);
        }
        private List<TypeOfWork> typeWork;
        public List<TypeOfWork> TypeOfWorks
        {
            get => typeWork; set => Set(ref typeWork, value);
        }
        private object projectItem;
        public object ProjectItem
        {
            get => projectItem;
            set => Set(ref projectItem, value); 
        }
        #endregion

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

        private ProjectDatum _selectProjectRequest;
        public ProjectDatum SelectProjectRequest
        {
            get => _selectProjectRequest;
            set
            {
                Set(ref _selectProjectRequest, value);
                ValidateProject();
            }
        }
        private StatusProject selectSatus;
        public StatusProject SelectSatus
        {
            get => selectSatus;
            set
            {
                Set(ref selectSatus, value);
                ValidateStatus();
            }
        }
        private Post selectPost;
        public Post SelectPost
        {
            get => selectPost;
            set
            {
                Set(ref selectPost, value);
                ValidatePost();
            }
        }
        private TypeOfWork selectTypeOfWork;
        public TypeOfWork SelectTypeOfWork
        {
            get => selectTypeOfWork;
            set
            {
                Set(ref selectTypeOfWork, value);
                ValidateTurbineName();
            }
        }

        #region BoolValidation
        private bool _checkName;
        private bool _checkDescription;
        private bool _checkStatus;
        private bool _checkDateStart;
        private bool _checkDateEnd;
        private bool _checkTypeOfWork;
        private bool _checkPost;
        private bool _checkProject;
        #endregion

        #endregion

        #region Validation

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }


        private void ValidateDescription()
        {
            ClearErrors(nameof(RequestDescriptions));
            if (string.IsNullOrWhiteSpace(RequestDescriptions))
            {
                ClearErrors(nameof(RequestDescriptions));
                AddError(nameof(RequestDescriptions), "*Поле не может быть пустым.");
                _checkDescription = false;
            }
            else
            {
                _checkDescription = true;
            }
        }

        private void ValidateTurbineName()
        {
            ClearErrors(nameof(RequestName));
            if (string.IsNullOrWhiteSpace(RequestName))
            {
                ClearErrors(nameof(RequestName));
                AddError(nameof(RequestName), "*Поле не может быть пустым.");
                _checkName = false;

            }
            else
            {
                _checkName = true;
            }
        }

        private void ValidatePost()
        {
            ClearErrors(nameof(SelectPost));
            if ((SelectPost) == null)
            {
                ClearErrors(nameof(SelectPost));
                AddError(nameof(SelectPost), "*Не выбран исполнитель.");
                _checkPost = false;
            }
            else
            {
                _checkPost = true;
            }

        }

        private void ValidateStatus()
        {
            ClearErrors(nameof(SelectSatus));
            if ((SelectSatus) == null)
            {
                ClearErrors(nameof(SelectSatus));
                AddError(nameof(SelectSatus), "*Не выбран исполнитель.");
                _checkStatus = false;
            }
            else
            {
                _checkStatus = true;
            }
        }

        private void ValidateTypeWork()
        {
            ClearErrors(nameof(SelectTypeOfWork));
            if ((SelectTypeOfWork) == null)
            {
                ClearErrors(nameof(SelectTypeOfWork));
                AddError(nameof(SelectTypeOfWork), "*Не выбран исполнитель.");
                _checkTypeOfWork = false;
            }
            else
            {
                _checkTypeOfWork = true;
            }
        }

        private void ValidateProject()
        {
            ClearErrors(nameof(SelectProjectRequest));
            if ((SelectProjectRequest) == null)
            {
                ClearErrors(nameof(SelectProjectRequest));
                AddError(nameof(SelectProjectRequest), "*Не выбран исполнитель.");
                _checkProject = false;
            }
            else
            {
                _checkProject = true;
            }
        }

        private void ValidateDateStart()
        {
            ClearErrors(nameof(DateStart));
            if (string.IsNullOrWhiteSpace(DateStart))
            {
                ClearErrors(nameof(DateStart));
                AddError(nameof(DateStart), "*Не выбрана дата.");
                _checkDateStart = false;

            }
            else if (Convert.ToDateTime(DateStart) >= Convert.ToDateTime(DateEnd))
            {
                ClearErrors(nameof(DateStart));
                AddError(nameof(DateStart), "*Дата начала не может превышать дату окончания.");
                _checkDateStart = false;
            }
            else
            {
                _checkDateStart = true;
            }


        }

        private void ValidateDateEnd()
        {
            ClearErrors(nameof(DateEnd));
            if (string.IsNullOrWhiteSpace(DateEnd))
            {
                ClearErrors(nameof(DateStart));
                AddError(nameof(DateStart), "*Не выбрана дата.");
                _checkDateEnd = false;

            }
            else if (Convert.ToDateTime(DateEnd) <= Convert.ToDateTime(DateStart))
            {
                ClearErrors(nameof(DateEnd));
                AddError(nameof(DateEnd), "*Дата окончания не может быть меньше даты начала.");
                _checkDateEnd = false;
            }
            else
            {
                _checkDateEnd = true;
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
            FiledAddOrUpdateContent = "";
            ClearErrors(nameof(RequestName));
            ClearErrors(nameof(RequestDescriptions));
            ClearErrors(nameof(SelectProjectRequest));
            ClearErrors(nameof(SelectPost));
            ClearErrors(nameof(SelectSatus));
            ClearErrors(nameof(SelectTypeOfWork));
            ClearErrors(nameof(DateStart));
            ClearErrors(nameof(DateEnd));
            timer.Stop();
        }
        private void RefreshContent(object e, object sender)
        {
            _checkName = false;
            _checkDescription = false;
            _checkProject = false;
            _checkPost = false;
            _checkStatus = false;
            _checkTypeOfWork = false;
            RequestName = string.Empty;
            RequestDescriptions = string.Empty;
            SelectProjectRequest = null;
            SelectPost = null;
            SelectSatus = null;
            SelectTypeOfWork = null;
            DateStart = DateTime.Now.ToShortDateString();
            DateEnd = DateTime.Now.ToShortDateString();
            FiledAddOrUpdateContent = string.Empty;
            timer.Stop();
        }
        private void OpenRequestList(object sender, object e)
        {
            MainVM.mainVM.MainCurrentControl = new RequestVM();
            timer.Stop();
        }
        #endregion
    }
}
