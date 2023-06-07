using ScottPlot.Palettes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateOraganization : Base.ViewModel, INotifyDataErrorInfo
    {
        private static string patternRegex = "^[a-zA-Zа-яА-Я]+$";
        private static string numPattern = "^[0-9]+$";

        #region Property

        #region OraganizationField
        private string _organizationNames;
        public string OrganizationNames
        {
            get => _organizationNames;
            set
            {
                Set(ref _organizationNames, value);
                ValidateName();
            }
        }
        private string _organizationTown;
        public string OrganizationTown
        {
            get => _organizationTown;
            set
            {
                Set(ref _organizationTown, value);
                ValidateTown();
            }
        }
        private string _organizationStreet;
        public string OrganizationStreet
        {
            get => _organizationStreet;
            set
            {
                Set(ref _organizationStreet, value);
                ValidateStreet();
            }
        }
        private string _organizationBuilder;
        public string OrganizationBuilder
        {
            get => _organizationBuilder;
            set
            {
                Set(ref _organizationBuilder, value);
                ValidateBuilder();
            }
        }
        #endregion

        #region ValidationCheck
        private bool _checkOrganizationNames;
        public bool CheckOrganizationNames
        {
            get => _checkOrganizationNames;
            set => Set(ref _checkOrganizationNames, value);
        }
        private bool _checkOrganizationTown;
        public bool CheckOrganizationTown
        {
            get => _checkOrganizationTown;
            set => Set(ref _checkOrganizationTown, value);
        }
        private bool _checkOrganizationBuilder;
        public bool CheckOrganizationBuilder
        {
            get => _checkOrganizationBuilder; 
            set => Set(ref _checkOrganizationBuilder, value);
        }
        private bool _checkOrganizationStreet;
        public bool CheckOrganizationStreet
        {
            get => _checkOrganizationStreet; 
            set => Set(ref _checkOrganizationStreet, value);
        }
        #endregion

        private string _contentButton;
        public string ContentButton
        {
            get => _contentButton;
            set => Set(ref _contentButton, value);
        }

        private string _failedAddOrUpdate;
        public string FailedAddOrUpdate
        {
            get => _failedAddOrUpdate;
            set => Set(ref _failedAddOrUpdate, value);
        }

        private decimal _foregroundFailedMessage;
        public decimal ForegroundFailedMessage
        {
            get => _foregroundFailedMessage;
            set => Set(ref _foregroundFailedMessage, value);
        }    
        #endregion

        #region Command
        public ICommand CreateOrUpdateOraganizations { get; }
        private bool CanCreateOrUpdateOraganizationsExecution(object parametr) => true;
        private async void OnCreateOrUpdateOraganizationsExecution(object paramert)
        {
            if(MainWindowViewModel.main.UpdOraganization != null)
            {
                #region ValidationPreUpdate
                ValidateName();
                ValidateTown();
                ValidateBuilder();
                ValidateStreet();
                #endregion
                if(CheckOrganizationNames && CheckOrganizationTown && CheckOrganizationStreet && CheckOrganizationBuilder)
                {
                    Oraganization updOraganization = MainWindowViewModel.main.UpdOraganization;
                    updOraganization.OraganizationName = OrganizationNames;
                    updOraganization.OraganizationAdres = "г. " + OrganizationTown + ", ул. " + OrganizationStreet + ", д. " + OrganizationBuilder;
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdate = "Данные организации обновлены";
                    ForegroundFailedMessage = 1;

                }
                else
                {
                    FailedAddOrUpdate = "Не удалось обновить данные, провертe правильность ввода";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                   
                }
            }
            else
            {
                #region ValidationPreCreate
                ValidateName();
                ValidateTown();
                ValidateBuilder();
                ValidateStreet();
                #endregion
                if (CheckOrganizationNames && CheckOrganizationTown && CheckOrganizationStreet && CheckOrganizationBuilder)
                {
                    Oraganization newOraganization = new Oraganization() {
                        OraganizationName = OrganizationNames,
                        OraganizationAdres = "г. " + OrganizationTown + ", ул. " + OrganizationStreet + ", д. " + OrganizationBuilder,
                        DeleteOrganization = false
                    };
                    MainWindowViewModel.context.Oraganizations.Add(newOraganization);
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdate = "Организация добавлена";
                    ForegroundFailedMessage = 1;
                    Thread.Sleep(1000);
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshContent;
                    timer.Start();
                }
                else
                {
                    FailedAddOrUpdate = "Не удалось добавить данные, провертe правильность ввода";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
        }

        public ICommand BackCustomerList { get; }
        private bool CanBackCustomerListExecute(object parametr) => true;
        private void OnBackCustomerListExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new CustomerListVM();
        }
        #endregion

        public CreateOrUpdateOraganization() 
        { 
            if(MainWindowViewModel.main.UpdOraganization != null)
            {
                OrganizationNames = MainWindowViewModel.main.UpdOraganization.OraganizationName;
                string[] fullAdress = MainWindowViewModel.main.UpdOraganization.OraganizationAdres.Split(',');
                string[] town = fullAdress[0].Split('.');
                string[] street = fullAdress[1].Split(".");
                string[] builder = fullAdress[2].Split(".");
                OrganizationTown = town[1].Trim();
                OrganizationStreet = street[1].Trim();
                OrganizationBuilder = builder[1].Trim();
                ContentButton = "Изменить";
            }
            else
            {
                ContentButton = "Добавить";
            }

            #region Command
            CreateOrUpdateOraganizations = new LambdaCommand(OnCreateOrUpdateOraganizationsExecution, CanCreateOrUpdateOraganizationsExecution);
            BackCustomerList = new LambdaCommand(OnBackCustomerListExecute, CanBackCustomerListExecute);
            #endregion
        }

        #region Validation


        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }


        private void ValidateTown()
        {
            ClearErrors(nameof(OrganizationTown));
            if (string.IsNullOrWhiteSpace(OrganizationTown) || !Regex.IsMatch(OrganizationTown, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(OrganizationTown));
                AddError(nameof(OrganizationTown), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckOrganizationTown = false;
            }
            else
            {

                CheckOrganizationTown = true;
            }

        }
        private void ValidateName()
        {
            ClearErrors(nameof(OrganizationNames));
            if (string.IsNullOrWhiteSpace(OrganizationNames) || !Regex.IsMatch(OrganizationNames, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(OrganizationNames));
                AddError(nameof(OrganizationNames), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckOrganizationNames = false;

            }
            else
            {

                CheckOrganizationNames = true;
            }

        }
        private void ValidateStreet()
        {
            ClearErrors(nameof(OrganizationStreet));
            if (string.IsNullOrWhiteSpace(OrganizationStreet) || !Regex.IsMatch(OrganizationStreet, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(OrganizationStreet));
                AddError(nameof(OrganizationStreet), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckOrganizationStreet = false;

            }
            else
            {

                CheckOrganizationStreet = true;
            }

        }
        private void ValidateBuilder()
        {
            ClearErrors(nameof(OrganizationBuilder));
            if (string.IsNullOrWhiteSpace(OrganizationBuilder) || !Regex.IsMatch(OrganizationBuilder, numPattern, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(OrganizationBuilder));
                AddError(nameof(OrganizationBuilder), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckOrganizationBuilder = false;

            }
            else
            {
                CheckOrganizationBuilder = true;
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
            FailedAddOrUpdate = "";
            ClearErrors(nameof(OrganizationNames));
            ClearErrors(nameof(OrganizationTown));
            ClearErrors(nameof(OrganizationStreet));
            ClearErrors(nameof(OrganizationBuilder));
            timer.Stop();
        }
        private void RefreshContent(object e, object sender) 
        {
            CheckOrganizationNames = false; 
            CheckOrganizationTown = false;
            CheckOrganizationStreet = false;
            CheckOrganizationBuilder = false;
            OrganizationNames = "";
            OrganizationTown = "";
            OrganizationBuilder = "";
            OrganizationStreet = "";
            FailedAddOrUpdate = string.Empty;
            timer.Stop();
        }
        private void OpenCustomerList(object sender, object e)
        {
            MainVM.mainVM.MainCurrentControl = new CustomerListVM();
            timer.Stop();
        }
        #endregion
    }

}
