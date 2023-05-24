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

        private List<Turbine>? _turbines;
        public List<Turbine>? Turbines
        {
            get => _turbines;
            set => Set(ref _turbines, value);
        }

        #endregion

        #region Property

        private Turbine? _selectTurbine;
        public Turbine? SelectTurbine
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
                FailedAddOrUpdateContent = "*Турбина не найдена";
                ForegroundFailedMessage = -1;
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
            else
            {
                FailedAddOrUpdateContent = "*Турбина не выбранна";
                ForegroundFailedMessage = -1;
            }
        }


        public ICommand? CreateTurbine { get; }

        private bool CanCreateTurbineExecute(object parameter) => true;
        private void OnCreateTurbineExecute(object parameter)
        {
            MainWindowViewModel.main.UpdTurbine = null;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateTurbineVM();
        }

        public ICommand DeleteTurbine { get; }
        private bool CanDeleteTurbineExecute(object parameter) => true;
        private async void OnDeleteTurbineExecute(object parametr)
        {
            if(SelectTurbine != null)
            {
                Turbine delTurbine = SelectTurbine;
                delTurbine.DeleteTurbine = true;
                await MainWindowViewModel.context.SaveChangesAsync();
                await MainWindowViewModel.main.UpdateData();
                Turbines = MainWindowViewModel.main.TurbinesAll.Where(x => x.DeleteTurbine == false).ToList();
                TurbineItem = Turbines;
                FailedAddOrUpdateContent = "*Турбина удалена";
                ForegroundFailedMessage = 1;
            }
            else
            {
                FailedAddOrUpdateContent = "*Турбина не выбранна";
                ForegroundFailedMessage = -1;
            }
        }

        public ICommand OpenTurbine { get; }
        private bool CanOpenTurbineExecute(object parameter) => true;
        private void OnOpenTurbineExecute(object parametr)
        {
            if (SelectTurbine != null)
            {
                MainWindowViewModel.main.OpenTurbine = SelectTurbine;
                MainVM.mainVM.MainCurrentControl = new TurbineContentVM();
            }
            else
            {
                FailedAddOrUpdateContent = "*Турбина не выбранна";
                ForegroundFailedMessage = -1;
            }
        }

        #endregion

        public TurbineVM() 
        { 

            Turbines = MainWindowViewModel.main.TurbinesAll.Where(x => x.DeleteTurbine == false).ToList();

            TurbineItem = Turbines;

            SearchTurbine = new LambdaCommand(OnSearchTurbineExecute, CanSearchTurbineExecute);

            UpdateTurbine = new LambdaCommand(OnUpdateTurbineExecute, CanUpdateTurbineExecute);

            CreateTurbine = new LambdaCommand(OnCreateTurbineExecute, CanCreateTurbineExecute);

            DeleteTurbine = new LambdaCommand(OnDeleteTurbineExecute, CanDeleteTurbineExecute);

            OpenTurbine = new LambdaCommand(OnOpenTurbineExecute, CanOpenTurbineExecute);
        }

        private decimal foregroundFailedMessage;

        public decimal ForegroundFailedMessage { get => foregroundFailedMessage; set => Set(ref foregroundFailedMessage, value); }

        private string failedAddOrUpdateContent;

        public string FailedAddOrUpdateContent { get => failedAddOrUpdateContent; set => Set(ref failedAddOrUpdateContent, value); }
    }
}
