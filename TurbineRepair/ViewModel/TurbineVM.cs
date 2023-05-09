using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.View.ContentGUI.MainContentGUI;

namespace TurbineRepair.ViewModel
{
    internal class TurbineVM : Base.ViewModel
    {

        #region List

        private List<Turbine> _turbines;
        public List<Turbine> Turbines
        {
            get => _turbines;
            set => Set(ref _turbines, value);
        }

        #endregion

        #region Property

        private Turbine _selectTurbine;
        public Turbine SelectTurbine
        {
            get => _selectTurbine;
            set => Set(ref _selectTurbine, value);
        }

        private string _searchTurbineName;
        public string SearchTurbineName
        {
            get => _searchTurbineName;
            set => Set(ref _searchTurbineName, value);
        }

        private object _turbineItem;
        public object TurbineItem
        {
            get => _turbineItem;
            set => Set(ref _turbineItem, value);
        }
        #endregion

        #region Command
        public ICommand SearchTurbine { get; }
        private bool CanSearchTurbineExecute(object parameter) => true;
        public void OnSearchTurbineExecute(object parametr)
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
        #endregion

        public TurbineVM() 
        { 
            Turbines = MainWindowViewModel.main.TurbinesAll.ToList();

            TurbineItem = Turbines;

            SearchTurbine = new LambdaCommand(OnSearchTurbineExecute,CanSearchTurbineExecute);
        }
    }
}
