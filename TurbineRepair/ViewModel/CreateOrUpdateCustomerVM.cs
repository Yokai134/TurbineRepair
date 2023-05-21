using Accessibility;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateCustomerVM : Base.ViewModel, INotifyDataErrorInfo
    {
        private string patternRegex = "^[a-zA-Zа-яА-Я]+$";

        #region Property
        private List<Oraganization> _oraganizations = MainWindowViewModel.main.Oraganizations;
        public List<Oraganization> Oraganizations
        {
            get => _oraganizations;
            set => Set(ref _oraganizations, value);
        }

        #region CustomerField
        private string _customerSurname;
        public string CustomerSurname
        {
            get => _customerSurname;
            set 
            { 
                Set(ref _customerSurname, value);
                ValidateSurname();
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get => _customerName;
            set
            {
                Set(ref _customerName, value);
                ValidateName();
            }
        }

        private string _customerPatronomyc;
        public string CustomerPatronomyc
        {
            get => _customerPatronomyc;
            set 
            {
                Set(ref _customerPatronomyc, value);
                ValidatePatronomyc();
            }
        }

        private Oraganization _selectOrganization;
        public Oraganization SelectOrganization
        {
            get => _selectOrganization;
            set 
            {
                Set(ref _selectOrganization, value);
                ValidateOrganization();
            } 
        }
        #endregion

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

        #region BoolValidate
        private bool _checkSurname = false;
        public bool CheckSurname
        {
            get => _checkSurname;
            set => Set(ref _checkSurname, value);
        }
        private bool _checkName = false;
        public bool CheckName
        {
            get => _checkName;
            set => Set(ref _checkName, value);
        }
        private bool _checkPatronomyc = false;
        public bool CheckPatronomyc
        {
            get => _checkPatronomyc;
            set => Set(ref _checkPatronomyc, value);
        }
        private bool _checkOrganization = false;
        public bool CheckOrganization
        {
            get => _checkOrganization;
            set => Set(ref _checkOrganization, value);
        }
        #endregion
        #endregion

        #region Command

        public ICommand CreateOrUpdateCustomer { get; }
        private bool CanCreateOrUpdateCustomerExecute(object parametr) => true;
        private async void OnCreateOrUpdateCustomerExecute(object parametr)
        {
            if(MainWindowViewModel.main.UpdCustomer != null)
            {

                ValidateName();
                ValidateSurname();
                ValidatePatronomyc();
                ValidateOrganization();
                if (CheckSurname && CheckName && CheckPatronomyc && CheckOrganization)
                {
                    Customer updCustomer = MainWindowViewModel.main.UpdCustomer;
                    updCustomer.CustomerSurname = CustomerSurname;
                    updCustomer.CustomerName = CustomerName;
                    updCustomer.CustomerPatronomyc = CustomerPatronomyc;
                    updCustomer.CustomerOrganization = SelectOrganization.Id;
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdateContent = "Данные обновлены";
                    ForegroundFailedMessage = 1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += OpenCustomerList;
                    timer.Start();
                }
                else
                {
                    FailedAddOrUpdateContent = "*Не удалось обновить данные";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
            else
            {
                ValidateName();
                ValidateSurname();
                ValidatePatronomyc();
                ValidateOrganization();
                if (CheckSurname && CheckName && CheckPatronomyc && CheckOrganization)
                {
                    Customer newCustomer = new Customer()
                    {
                        CustomerSurname = CustomerSurname,
                        CustomerName = CustomerName,
                        CustomerPatronomyc = CustomerPatronomyc,
                        CustomerOrganization = SelectOrganization.Id,
                        DeleteCustomer = false
                        
                    };
                    MainWindowViewModel.context.Customers.Add(newCustomer);
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdateContent = "Добавлен новый заказчик";
                    ForegroundFailedMessage = 1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshContent;
                    timer.Start();
                }
                else
                {
                    FailedAddOrUpdateContent = "*Не удалось добавить данные";
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

        public CreateOrUpdateCustomerVM() 
        { 
            if(MainWindowViewModel.main.UpdCustomer != null) 
            {
                CustomerSurname = MainWindowViewModel.main.UpdCustomer.CustomerSurname;
                CustomerName = MainWindowViewModel.main.UpdCustomer.CustomerName;
                CustomerPatronomyc = MainWindowViewModel.main.UpdCustomer.CustomerPatronomyc;
                SelectOrganization = MainWindowViewModel.main.UpdCustomer.CustomerOrganizationNavigation;
                ContentButton = "Изменить";
            }
            else
            {
                ContentButton = "Добавить";
            }

            CreateOrUpdateCustomer = new LambdaCommand(OnCreateOrUpdateCustomerExecute, CanCreateOrUpdateCustomerExecute);
            BackCustomerList = new LambdaCommand(OnBackCustomerListExecute, CanBackCustomerListExecute);
        }

        #region Validation

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }


        private void ValidateSurname()
        {
            ClearErrors(nameof(CustomerSurname));
            if (string.IsNullOrWhiteSpace(CustomerSurname) || !Regex.IsMatch(CustomerSurname, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(CustomerSurname));
                AddError(nameof(CustomerSurname), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckSurname = false;
            }
            else
            {
                CheckSurname = true;
            }

        }
        private void ValidateName()
        {
            ClearErrors(nameof(CustomerName));
            if (string.IsNullOrWhiteSpace(CustomerName) || !Regex.IsMatch(CustomerName, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(CustomerName));
                AddError(nameof(CustomerName), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckName = false;

            }
            else
            {
                CheckName = true;
            }

        }
        private void ValidatePatronomyc()
        {
            ClearErrors(nameof(CustomerPatronomyc));
            if (string.IsNullOrWhiteSpace(CustomerPatronomyc) || !Regex.IsMatch(CustomerPatronomyc, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(CustomerPatronomyc));
                AddError(nameof(CustomerPatronomyc), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckPatronomyc = false;

            }
            else
            {
                CheckPatronomyc = true;
            }

        }
        private void ValidateOrganization()
        {
            ClearErrors(nameof(SelectOrganization));
            if (SelectOrganization == null)
            {
                ClearErrors(nameof(SelectOrganization));
                AddError(nameof(SelectOrganization), "*Не выбрана организация.");
                CheckOrganization = false;
            }
            else
            {
                CheckOrganization = true;
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
            ClearErrors(nameof(CustomerSurname));
            ClearErrors(nameof(CustomerName));
            ClearErrors(nameof(CustomerPatronomyc));
            ClearErrors(nameof(SelectOrganization));
            timer.Stop();
        }
        private void RefreshContent(object e, object sender)
        {
            CheckName = false;
            CheckOrganization = false;
            CheckSurname = false;
            CheckPatronomyc = false;
            CustomerSurname = "";
            CustomerName = "";
            CustomerPatronomyc = "";
            SelectOrganization = null;
            FailedAddOrUpdateContent = string.Empty;
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
