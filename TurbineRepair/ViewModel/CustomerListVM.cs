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
                    CustomerItem = CustomerList.Where(x => x.CustomerSurname.StartsWith(SearchCustomer)).ToList();
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
                    OrganizationItem = OrganizationList.Where(x => x.OraganizationName.StartsWith(SearchOrganization)).ToList();
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
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateCustomerVM();
        }

        public ICommand UpdateCustomer { get; }
        private bool CanUpdateCustomerExecute(object parameter) => true;
        private void OnUpdateCustomerExecute(object parametr)
        {
            MainWindowViewModel.main.UpdCustomer = SelectCustomer;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateCustomerVM();
        }
        #endregion

        public CustomerListVM() 
        { 
            CustomerList = MainWindowViewModel.main.CustomersAll.ToList();
            CustomerItem = CustomerList;

            OrganizationList = MainWindowViewModel.main.Oraganizations.ToList();
            OrganizationItem = OrganizationList;

            SearchCustomerCommand = new LambdaCommand(OnSearchCustomerCommandExecute, CanSearchCustomerCommandExecute);
            SearchOrganizationCommand = new LambdaCommand(OnSearchOrganizationCommandExecute, CanSearchOrganizationCommandExecute);
            CreateCustomer = new LambdaCommand(OnCreateCustomerExecute, CanCreateCustomerExecute);
            UpdateCustomer = new LambdaCommand(OnUpdateCustomerExecute, CanUpdateCustomerExecute);
        }
    }
}
