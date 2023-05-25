using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;
using TurbineRepair.View.ContentGUI.MainContentGUI;

namespace TurbineRepair.ViewModel
{
    internal class CustomerListVM : Base.ViewModel
    {

        #region List
        private List<Customer> _customerList;
        public List<Customer> CustomerList
        {
            get => _customerList;
            set => Set(ref _customerList, value);
        }

        private object _customerItem;
        public object CustomerItem
        {
            get => _customerItem;
            set => Set(ref _customerItem, value);
        }

        private List<Oraganization> _organizationList;
        public List <Oraganization> OrganizationList
        {
            get => _organizationList;
            set => Set(ref _organizationList, value);
        }

        private object _organizationItem;
        public object OrganizationItem
        {
            get => _organizationItem;
            set => Set(ref _organizationItem, value);
        }
        #endregion

        #region Property
        private string _searchCustomer;
        public string SearchCustomer
        {
            get => _searchCustomer;
            set => Set(ref _searchCustomer, value);
        }
        private string _searchOrganization;
        public string SearchOrganization
        {
            get => _searchOrganization; 
            set => Set(ref _searchOrganization, value);
        }

        private Customer _selectCustomer;
        public Customer SelectCustomer
        {
            get => _selectCustomer;
            set => Set(ref _selectCustomer, value);
        }

        private string _noSelectionCustomer;
        public string NoSelectionCustomer
        {
            get => _noSelectionCustomer;
            set => Set(ref _noSelectionCustomer, value);
        }

        private Oraganization _selectedOrganization;
        public Oraganization SelectedOrganization
        {
            get => _selectedOrganization;
            set => Set(ref _selectedOrganization, value); 
        }
        private string _noSelectedOrganization;
        public string NoSelectedOrganization
        {
            get => _noSelectedOrganization;
            set => Set(ref _noSelectedOrganization, value);
        }

        private decimal _foregroundFailedMessage;
        public decimal ForegroundFailedMessage
        {
            get => _foregroundFailedMessage;
            set => Set(ref _foregroundFailedMessage, value);
        }
        #endregion

        #region Command
        public ICommand SearchCustomerCommand { get; }
        private bool CanSearchCustomerCommandExecute(object parameter) => true;
        private void OnSearchCustomerCommandExecute(object parametr)
        {
            try
            {

                if (SearchCustomer != null)
                {
                    CustomerItem = CustomerList.Where(x => x.CustomerSurname.StartsWith(SearchCustomer) && x.DeleteCustomer == false && x.CustomerOrganizationNavigation.DeleteOrganization == false).ToList();
                }
                else if (SearchCustomer == "") CustomerItem = CustomerList;
            }
            catch
            {
                MessageBox.Show("Заказчик не найден", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        public ICommand SearchOrganizationCommand { get; }
        private bool CanSearchOrganizationCommandExecute(object parameter) => true;
        private void OnSearchOrganizationCommandExecute(object parametr)
        {
            try
            {

                if (SearchOrganization != null)
                {
                    OrganizationItem = OrganizationList.Where(x => x.OraganizationName.StartsWith(SearchOrganization) && x.DeleteOrganization == false).ToList();
                }
                else if (SearchOrganization == "") OrganizationItem = OrganizationList;
            }
            catch
            {
                MessageBox.Show("Организация не найден", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        public ICommand CreateCustomer {  get; }
        private bool CanCreateCustomerExecute(object parameter) => true;
        private void OnCreateCustomerExecute(object parametr)
        {
            MainWindowViewModel.main.UpdCustomer = null;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateCustomerVM();
        }

        public ICommand UpdateCustomer { get; }
        private bool CanUpdateCustomerExecute(object parameter) => true;
        private void OnUpdateCustomerExecute(object parametr)
        {
            if (SelectCustomer != null)
            {
                MainWindowViewModel.main.UpdCustomer = SelectCustomer;
                MainVM.mainVM.MainCurrentControl = new CreateOrUpdateCustomerVM();
                NoSelectionCustomer = "";
            }
            else 
            { 
                NoSelectionCustomer = "*Не выбран заказчик";
                ForegroundFailedMessage = -1;
            }
           

        }

        public ICommand DeleteCustomer { get; }
        private bool CanDeleteCustomerExecute(object parameter) => true; 
        private async void OnDeleteCustomerExecute(object parametr)
        {
            if (SelectCustomer != null)
            {
                Customer delCustomer = SelectCustomer;
                delCustomer.DeleteCustomer = true;
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                ForegroundFailedMessage = 1;
                CustomerList = MainWindowViewModel.main.CustomersAll.Where(x => x.DeleteCustomer == false && x.CustomerOrganizationNavigation.DeleteOrganization == false).ToList();
                CustomerItem = CustomerList;
                NoSelectionCustomer = "*Готово";
                SelectCustomer = null;

            }
            else
            {
                NoSelectionCustomer = "*Не выбран заказчик";
                ForegroundFailedMessage = -1;
            }
        }


        public ICommand CreateOrganization { get; }
        private bool CanCreateOrganizationExecute(object parameter) => true;
        private void OnCreateOrganizationExecute(object parametr)
        {
            MainWindowViewModel.main.UpdOraganization = null;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateOraganization();
        }

        public ICommand UpdateOrganization { get; }
        private bool CanUpdateOrganizationExecute(object parametr) => true;
        private void OnUpdateOrganizationExecute(object parametr)
        {
            if (SelectedOrganization != null)
            {
                MainWindowViewModel.main.UpdOraganization = SelectedOrganization;
                MainVM.mainVM.MainCurrentControl = new CreateOrUpdateOraganization();
                NoSelectedOrganization = "";
            }
            else
            {
                ForegroundFailedMessage = -1;
                NoSelectedOrganization = "*Не выбрана организация";
            }
        }

        public ICommand DeleteOrganization { get; }
        private bool CanDeleteOrganizationExecute(object parametr) => true;
        private async void OnDeleteOrganizationExecute(object parametr)
        {
            if (SelectedOrganization != null)
            {
                Oraganization delOrganization = SelectedOrganization;
                delOrganization.DeleteOrganization = true;
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                ForegroundFailedMessage = 1;
                CustomerList = MainWindowViewModel.main.CustomersAll.Where(x => x.DeleteCustomer == false && x.CustomerOrganizationNavigation.DeleteOrganization == false).ToList();
                CustomerItem = CustomerList;
                OrganizationList = MainWindowViewModel.main.Oraganizations.Where(x => x.DeleteOrganization == false).ToList();
                OrganizationItem = OrganizationList;
                NoSelectedOrganization = "Готово";
                SelectedOrganization = null;
            }
            else
            {
                ForegroundFailedMessage = -1;
                NoSelectedOrganization = "*Не выбрана организация";
            }

        }
        #endregion


        public CustomerListVM() 
        { 
            CustomerList = MainWindowViewModel.main.CustomersAll.Where(x=>x.DeleteCustomer == false && x.CustomerOrganizationNavigation.DeleteOrganization == false).ToList();
            CustomerItem = CustomerList;

            OrganizationList = MainWindowViewModel.main.Oraganizations.Where(x => x.DeleteOrganization == false).ToList();
            OrganizationItem = OrganizationList;

            #region Command
            SearchCustomerCommand = new LambdaCommand(OnSearchCustomerCommandExecute, CanSearchCustomerCommandExecute);
            SearchOrganizationCommand = new LambdaCommand(OnSearchOrganizationCommandExecute, CanSearchOrganizationCommandExecute);
            CreateCustomer = new LambdaCommand(OnCreateCustomerExecute, CanCreateCustomerExecute);
            UpdateCustomer = new LambdaCommand(OnUpdateCustomerExecute, CanUpdateCustomerExecute);
            DeleteCustomer = new LambdaCommand(OnDeleteCustomerExecute, CanDeleteCustomerExecute);
            CreateOrganization = new LambdaCommand(OnCreateOrganizationExecute, CanCreateOrganizationExecute);
            UpdateOrganization = new LambdaCommand(OnUpdateOrganizationExecute, CanUpdateOrganizationExecute);
            DeleteOrganization = new LambdaCommand(OnDeleteOrganizationExecute, CanDeleteOrganizationExecute);
            #endregion
        }
    }
}
