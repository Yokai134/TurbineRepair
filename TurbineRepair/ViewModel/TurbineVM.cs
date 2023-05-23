using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;
using TurbineRepair.View.ContentGUI.MainContentGUI;

namespace TurbineRepair.ViewModel
{
    internal class TurbineVM : Base.ViewModel
    {

        #region List

        private List<Model.Turbine>? _turbines;
        public List<Model.Turbine>? Turbines
        {
            get => _turbines;
            set => Set(ref _turbines, value);
        }

        #endregion

        #region Property

        private Model.Turbine? _selectTurbine;
        public Model.Turbine? SelectTurbine
        {
            get => _selectTurbine;
            set => Set(ref _selectTurbine, value);
        }

        private string? _searchTurbineName;
        public string? SearchTurbineName
        {
            get => _searchTurbineName;
            set => Set(ref _searchTurbineName, value);
        }

        private object? _turbineItem;
        public object? TurbineItem
        {
            get => _turbineItem;
            set => Set(ref _turbineItem, value);
        }
        #endregion

        #region Command
        public ICommand SearchTurbine { get; }
        private bool CanSearchTurbineExecute(object parameter) => true;
        private void OnSearchTurbineExecute(object parametr)
        {
            try
            {

                if (SearchTurbineName != null)
                {
                    TurbineItem = Turbines.Where(x=>x.TurbineName.StartsWith(SearchTurbineName)).ToList();
                }
                else if (SearchTurbineName == "") TurbineItem = Turbines;
            }
            catch
            {
                MessageBox.Show("Исполнитель не найден", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }


        public ICommand? UpdateTurbine { get; }
        private bool CanUpdateTurbineExecute(object parameter) => true;
        private void OnUpdateTurbineExecute(object parametr)
        {
            if (SelectTurbine != null)
            {
                MainWindowViewModel.main.UpdTurbine = SelectTurbine;
                MainVM.mainVM.MainCurrentControl = new CreateOrUpdateTurbineVM();
            }
        }


        public ICommand? CreateTurbine { get; }

        private bool CanCreateTurbineExecute(object parameter) => true;
        private void OnCreateTurbineExecute(object parameter)
        {
            MainWindowViewModel.main.UpdTurbine = null;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateTurbineVM();
        }

        #endregion

        public TurbineVM() 
        { 

            Turbines = MainWindowViewModel.main.TurbinesAll;

            TurbineItem = Turbines;

            SearchTurbine = new LambdaCommand(OnSearchTurbineExecute, CanSearchTurbineExecute);

            UpdateTurbine = new LambdaCommand(OnUpdateTurbineExecute, CanUpdateTurbineExecute);

            CreateTurbine = new LambdaCommand(OnCreateTurbineExecute, CanCreateTurbineExecute);
        }
    }
}
