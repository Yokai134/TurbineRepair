using Microsoft.Win32;
using System;
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
    internal class CreateOrUpdateTurbineVM : Base.ViewModel
    {

        #region Property
        private Turbine? _turbineData;
        public Turbine? TurbineData
        {
            get => _turbineData;
            set => Set(ref _turbineData, value);
        }

        private TurbineImage? _turbineImage;
        public TurbineImage? TurbineImage
        {
            get => _turbineImage;
            set => Set(ref _turbineImage, value);
        }

        private int _countImage = 0;
        public int CountImage
        {
            get => _countImage;
            set => Set(ref _countImage, value);
        }
        #endregion

        #region Command

        public ICommand CreateOrUpdate { get; }
        private bool CanCreateOrUpdateExecute(object parameter) => true;
        private async void OnCreateOrUpdateExecute(object parameter)
        {
            if (MainWindowViewModel.main.UpdTurbine != null)
            {
                Turbine turbine = TurbineData;
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                
            }
            else
            {
                Turbine turbine = new Turbine{
                    TurbineName = TurbineData.TurbineName,
                    TurbineImageNavigation = TurbineImage,

                };             
            }
        }


        public ICommand OpenImage { get; }
        private bool CanOpenImageExecute(object parameter) => true;
        private void OnOpenImageExecute(object parametr)
        {
            if(MainWindowViewModel.main.UpdTurbine != null)
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
                openFileDialog.ShowDialog();
                switch (CountImage)
                {
                    case 0:
                        TurbineData.TurbineImageNavigation.ImageOne = openFileDialog.FileName;
                        CountImage++;
                        break;
                    case 1:
                        TurbineData.TurbineImageNavigation.ImageTwo = openFileDialog.FileName;
                        CountImage++;
                        break;
                    case 2:
                        TurbineData.TurbineImageNavigation.ImageThree = openFileDialog.FileName;
                        CountImage++;
                        break;
                    case 3:
                        TurbineData.TurbineImageNavigation.ImageFour = openFileDialog.FileName;
                        CountImage = 0;
                        break;
                }
            }
            else
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
                openFileDialog.ShowDialog();
                switch (CountImage)
                {
                    case 0:
                        TurbineImage.ImageOne = openFileDialog.FileName;
                        CountImage++;
                        break;
                    case 1:
                        TurbineImage.ImageTwo = openFileDialog.FileName;
                        CountImage++;
                        break;
                    case 2:
                        TurbineImage.ImageThree = openFileDialog.FileName;
                        CountImage++;
                        break;
                    case 3:
                        TurbineImage.ImageFour = openFileDialog.FileName;
                        CountImage = 0;
                        break;
                }
            }
           
            
        }

        #endregion

        public CreateOrUpdateTurbineVM() 
        {
            if(MainWindowViewModel.main.UpdTurbine != null)
            {
                TurbineData = MainWindowViewModel.main.UpdTurbine;
                TurbineImage = MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation;
            }
            else 
            { 
                TurbineData = new Turbine();
                TurbineImage = new TurbineImage();
                
            }

            #region Command
            CreateOrUpdate = new LambdaCommand(OnCreateOrUpdateExecute, CanCreateOrUpdateExecute);

            OpenImage = new LambdaCommand(OnOpenImageExecute, CanOpenImageExecute);
            #endregion

        }
    }
}
