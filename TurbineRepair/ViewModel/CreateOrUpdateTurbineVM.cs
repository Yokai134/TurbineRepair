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
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateTurbineVM : Base.ViewModel, INotifyDataErrorInfo
    {
        private string pathProject = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\..\\..\\" + @"TurbineResource");
        #region Regex
        string decimalNumber = @"^\d+([.,]\d+?)?$";
        #endregion


        #region Property

        private int _countImage = 0;
        public int CountImage
        {
            get => _countImage;
            set => Set(ref _countImage, value);
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => Set(ref _imagePath, value);
        }

        private bool _imageOneUpd;
        public bool ImageOneUpd
        {
            get => _imageOneUpd;
            set => Set(ref _imageOneUpd, value);
        }

        private bool _imageTwoUpd;
        public bool ImageTwoUpd
        {
            get => _imageTwoUpd;
            set => Set(ref _imageTwoUpd, value);
        }

        private bool _imageThreeUpd;
        public bool ImageThreeUpd
        {
            get => _imageThreeUpd;
            set => Set(ref _imageThreeUpd, value);
        }

        private bool _imageFourUpd;
        public bool ImageFourUpd
        {
            get => _imageFourUpd;
            set => Set(ref _imageFourUpd, value);
        }

        private string _originalName;
        public string OriginalName
        {
            get => _originalName;
            set => Set(ref _originalName, value);
        }

        private string _contentButton;
        public string ContentButton
        {
            get => _contentButton;
            set => Set(ref _contentButton, value);
        }
        #endregion

        #region Command


        public ICommand CreateOrUpdate { get; }
        private bool CanCreateOrUpdateExecute(object parameter) => true;
        private async void OnCreateOrUpdateExecute(object parameter)
        {
            if (MainWindowViewModel.main.UpdTurbine != null)
            {
                Turbine updTurbine = MainWindowViewModel.main.UpdTurbine;
                updTurbine.TurbineName = TurbineNames;
                updTurbine.TurbineDescription = TurbineDescriptions;
                #region TurbineScpg
                updTurbine.TurbineScpgNavigation.PowerOutput = PowerOutputScpg;
                updTurbine.TurbineScpgNavigation.Fuel = FuelScpg;
                updTurbine.TurbineScpgNavigation.Frequency = FrequencyScpg;
                updTurbine.TurbineScpgNavigation.GrossEfficiency = GrossEfficiencyScpg;
                updTurbine.TurbineScpgNavigation.HeatRate = HeatRateScpg;
                updTurbine.TurbineScpgNavigation.TurbineSpeed = TurbineSpeedScpg;
                updTurbine.TurbineScpgNavigation.PressureRatio = PressureRatioScpg;
                updTurbine.TurbineScpgNavigation.ExhaustMassFlow = ExhaustMassFlowScpg;
                updTurbine.TurbineScpgNavigation.ExhaustTemperature = ExhaustTemperatureScpg;
                updTurbine.TurbineScpgNavigation.Emissions = EmmisionScpg;
                #endregion
                #region TurbineMda
                updTurbine.TurbineMdaNavigation.PowerOutput = PowerOutputMda;
                updTurbine.TurbineMdaNavigation.Fuel = FuelMda;
                updTurbine.TurbineMdaNavigation.Efficiency = EfficiencyMda;
                updTurbine.TurbineMdaNavigation.HeatRate = HeatRateMda;
                updTurbine.TurbineMdaNavigation.DriveShaftSpeed = DriveShaftSpeedMda;
                updTurbine.TurbineMdaNavigation.PressureRatio = PressureRatioMda;
                updTurbine.TurbineMdaNavigation.ExhaustMassFlow = ExhaustMassFlowMda;
                updTurbine.TurbineMdaNavigation.ExhaustTemperature = ExhaustTemperatureMda;
                updTurbine.TurbineMdaNavigation.Emissions = EmmisionMda;
                #endregion
                #region TurbinePgp    
                updTurbine.TurbinePgpNavigation.Weight = WeightPgp;
                updTurbine.TurbinePgpNavigation.Lenght = LenghtPgp;
                updTurbine.TurbinePgpNavigation.Width = WidthPgp;
                updTurbine.TurbinePgpNavigation.Height = HeightPgp;
                #endregion
                #region TurbineMdp
                updTurbine.TurbineMdpNavigation.Weight = WeightMdp;
                updTurbine.TurbineMdpNavigation.Lenght = LenghtMdp;
                updTurbine.TurbineMdpNavigation.Width = WidthMdp;
                updTurbine.TurbineMdpNavigation.Height = HeightMdp;
                #endregion
                #region TurbineImage
                if (OriginalName != updTurbine.TurbineName)
                {
                    string basePath = Path.GetFullPath(pathProject + @"\" + OriginalName);
                    string movePath = Path.GetFullPath(pathProject + @"\" + updTurbine.TurbineName);
                }
                string imagePath = Path.GetFullPath(pathProject + @"\" + OriginalName);
                string[] imageFile = Directory.GetFiles(imagePath);
                if (ImageOneUpd)
                {
                    string copyPath = imageFile[0];
                    FileInfo fileInfo = new FileInfo(ImagePathOne);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                if (ImageTwoUpd)
                {

                    string copyPath = imageFile[1];
                    FileInfo fileInfo = new FileInfo(ImagePathTwo);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                if (ImageThreeUpd)
                {

                    string copyPath = imageFile[2];
                    FileInfo fileInfo = new FileInfo(ImagePathThree);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                if (ImageFourUpd)
                {

                    string copyPath = imageFile[3];
                    FileInfo fileInfo = new FileInfo(ImagePathFour);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }

                ImageOneUpd = false;
                ImageTwoUpd = false;
                ImageThreeUpd = false;
                ImageFourUpd = false;
                #endregion
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();

            }
            else
            {
                #region Create TurbineScpg
                TurbineScpg newTurbineScpg = new TurbineScpg()
                {
                    PowerOutput = PowerOutputScpg,
                    Fuel = FuelScpg,
                    Frequency = FrequencyScpg,
                    GrossEfficiency = GrossEfficiencyScpg,
                    HeatRate = HeatRateScpg,
                    TurbineSpeed = TurbineSpeedScpg,
                    PressureRatio = PressureRatioScpg,
                    ExhaustMassFlow = ExhaustMassFlowScpg,
                    ExhaustTemperature = ExhaustTemperatureScpg,
                    Emissions = EmmisionScpg

                };
                #endregion
                #region Create TurbineMda
                TurbineMdum newTurbineMda = new TurbineMdum()
                {
                    PowerOutput = PowerOutputMda,
                    Fuel = FuelMda,
                    Efficiency = EmmisionMda,
                    HeatRate = HeatRateMda,
                    DriveShaftSpeed = DriveShaftSpeedMda,
                    PressureRatio = PressureRatioMda,
                    ExhaustMassFlow = ExhaustMassFlowMda,
                    ExhaustTemperature = ExhaustTemperatureMda,
                    Emissions = EmmisionMda
                };
                #endregion
                #region Create TurbinePgp
                TurbinePgp newTurbinePgp = new TurbinePgp()
                {
                    Weight = WeightPgp,
                    Lenght = LenghtPgp,
                    Width = WidthPgp,
                    Height = HeightPgp,
                };
                #endregion
                #region Create TurbineMdp
                TurbineMdp newTurbineMdp = new TurbineMdp()
                {
                    Weight = WidthMdp,
                    Lenght = WidthMdp,
                    Width = WidthMdp,
                    Height = HeightMdp,
                };
                #endregion
                #region Create TurbineImage
                Directory.CreateDirectory(pathProject + @"\" + TurbineNames);
                for(int i = 0; i < 4; i++)
                {
                    var jpg = new Bitmap(256, 256);
                    using (var g = Graphics.FromImage(jpg))
                    {
                        g.Clear(Color.White);
                    }
                    jpg.Save(pathProject + @"\" + TurbineNames + @"\" + TurbineNames + "-" + (i + 1) + ".jpg");

                }

                string imagePath = Path.GetFullPath(pathProject + @"\" + TurbineNames);
                string[] imageFile = Directory.GetFiles(imagePath);
                if (ImageOneUpd)
                {
                    string copyPath = imageFile[0];
                    FileInfo fileInfo = new FileInfo(ImagePathOne);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                if (ImageTwoUpd)
                {

                    string copyPath = imageFile[1];
                    FileInfo fileInfo = new FileInfo(ImagePathTwo);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                if (ImageThreeUpd)
                {

                    string copyPath = imageFile[2];
                    FileInfo fileInfo = new FileInfo(ImagePathThree);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                if (ImageFourUpd)
                {

                    string copyPath = imageFile[3];
                    FileInfo fileInfo = new FileInfo(ImagePathFour);
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
                ImagePathOne = "\\" +TurbineNames + "\\" + TurbineNames + "-1.jpg";
                ImagePathTwo = "\\" + TurbineNames + "\\" + TurbineNames + "-2.jpg";
                ImagePathThree = "\\" + TurbineNames + "\\" + TurbineNames + "-3.jpg";
                ImagePathFour = "\\" + TurbineNames + "\\" + TurbineNames + "-4.jpg";
                ImageOneUpd = false;
                ImageTwoUpd = false;
                ImageThreeUpd = false;
                ImageFourUpd = false;
                TurbineImage newTurbineImage = new TurbineImage() 
                { 
                    ImageOne = ImagePathOne, 
                    ImageTwo = ImagePathTwo,
                    ImageThree = ImagePathThree,
                    ImageFour = ImagePathFour,
                };
                #endregion
                MainWindowViewModel.context.TurbineScpgs.Add(newTurbineScpg);
                MainWindowViewModel.context.TurbineMda.Add(newTurbineMda);
                MainWindowViewModel.context.TurbinePgps.Add(newTurbinePgp);
                MainWindowViewModel.context.TurbineMdps.Add(newTurbineMdp);
                MainWindowViewModel.context.TurbineImages.Add(newTurbineImage);
                MainWindowViewModel.context.SaveChanges();
                #region Create Turbine
                Turbine newTurbine = new Turbine()
                {
                    TurbineName = TurbineNames,
                    TurbineScpg = MainWindowViewModel.context.TurbineScpgs.Count(),
                    TurbineMda = MainWindowViewModel.context.TurbineMda.Count(),
                    TurbineDescription = TurbineDescriptions,
                    DeleteTurbine = false,
                    TurbinePgp = MainWindowViewModel.context.TurbinePgps.Count(),
                    TurbineMdp = MainWindowViewModel.context.TurbineMdps.Count(),
                    TurbineImage = MainWindowViewModel.context.TurbineImages.Count()

                };
                #endregion
                MainWindowViewModel.context.Turbines.Add(newTurbine);
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                
            }
        }


        public ICommand OpenImage { get; }
        private bool CanOpenImageExecute(object parameter) => true;
        private void OnOpenImageExecute(object parametr)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            ImagePath = openFileDialog.FileName;
            switch (CountImage)
            {
                case 0:
                    ImagePathOne = ImagePath;
                    ImageOneUpd = true;
                    CountImage++;
                    break;
                case 1:
                    ImageTwoUpd = true;
                    ImagePathTwo = ImagePath;
                    CountImage++;
                    break;
                case 2:
                    ImageThreeUpd = true;
                    ImagePathThree = ImagePath;
                    CountImage++;
                    break;
                case 3:
                    ImageFourUpd = true;
                    ImagePathFour = ImagePath;
                    CountImage = 0;
                    break;
            }
                                                 
        }

    

        #endregion

        #region Turbine Field

        #region Turbine Main

        private string _turbineNames;
        public string TurbineNames
        {
            get => _turbineNames; 
            set => Set(ref _turbineNames, value);
        }

        private string _turbineDescriptions;
        public string TurbineDescriptions
        {
            get => _turbineDescriptions;
            set => Set(ref _turbineDescriptions, value);
        }

        private bool _deleteTurbine;
        public bool DeleteTurbine
        {
            get => _deleteTurbine;
            set => Set(ref _deleteTurbine, value);
        }
        #endregion

        #region TurbineScpg

        private string _powerOutputScpg;
        public string PowerOutputScpg
        {
            get => _powerOutputScpg;
            set
            {
                Set(ref _powerOutputScpg, value);
                ValidateDecimalNumericPowerOutput();
            }
        }

        private string _fuelScpg;
        public string FuelScpg
        {
            get => _fuelScpg;
            set => Set(ref _fuelScpg, value);
        }

        private string _frequencyScpg;
        public string FrequencyScpg
        {
            get => _frequencyScpg;
            set => Set(ref _frequencyScpg, value);
        }

        private string _grossEfficiencyScpg;
        public string GrossEfficiencyScpg
        {
            get => _grossEfficiencyScpg;
            set => Set(ref _grossEfficiencyScpg, value);
        }

        private string _heatRateScpg;
        public string HeatRateScpg
        {
            get => _heatRateScpg;
            set => Set(ref _heatRateScpg, value);
        }

        private string _turbineSpeedScpg;
        public string TurbineSpeedScpg
        {
            get => _turbineSpeedScpg;
            set => Set(ref _turbineSpeedScpg, value);
        }

        private string _pressureRatioScpg;
        public string PressureRatioScpg
        {
            get => _pressureRatioScpg;
            set => Set(ref _pressureRatioScpg, value);
        }

        private string _exhaustMassFlowScpg;
        public string ExhaustMassFlowScpg
        {
            get => _exhaustMassFlowScpg;
            set => Set(ref _exhaustMassFlowScpg, value);
        }

        private string _exhaustTemperatureScpg;
        public string ExhaustTemperatureScpg
        {
            get => _exhaustTemperatureScpg;
            set => Set(ref _exhaustTemperatureScpg, value);
        }

        private string _emmisionScpg;
        public string EmmisionScpg
        {
            get => _emmisionScpg;
            set => Set(ref _emmisionScpg, value);
        }

        #endregion

        #region TurbineMda

        private string _powerOutputMda;
        public string PowerOutputMda
        {
            get => _powerOutputMda;
            set 
            {
                Set(ref _powerOutputMda, value);
                ValidateDecimalNumericPowerOutputMda();
                    
            }
        }

        private string _fuelMda;
        public string FuelMda
        {
            get => _fuelMda;
            set => Set(ref _fuelMda, value);
        }

        private string _efficiencyMda;
        public string EfficiencyMda
        {
            get => _efficiencyMda;
            set => Set(ref _efficiencyMda, value);
        }

        private string _heatRateMda;
        public string HeatRateMda
        {
            get => _heatRateMda;
            set => Set(ref _heatRateMda, value);
        }

        private string _driveShaftSpeedMda;
        public string DriveShaftSpeedMda
        {
            get => _driveShaftSpeedMda;
            set => Set(ref _driveShaftSpeedMda, value);
        }

        private string _pressureRatioMda;
        public string PressureRatioMda
        {
            get => _pressureRatioMda;
            set => Set(ref _pressureRatioMda, value);
        }

        private string _exhaustMassFlowMda;
        public string ExhaustMassFlowMda
        {
            get => _exhaustMassFlowMda;
            set => Set(ref _exhaustMassFlowMda, value);
        }

        private string _exhaustTemperatureMda;
        public string ExhaustTemperatureMda
        {
            get => _exhaustTemperatureMda;
            set => Set(ref _exhaustTemperatureMda, value);
        }

        private string _emmisionMda;
        public string EmmisionMda
        {
            get => _emmisionMda;
            set => Set(ref _emmisionMda, value);
        }

        #endregion

        #region TurbinePgp

        private string _weightPgp;
        public string WeightPgp
        {
            get => _weightPgp;
            set => Set(ref _weightPgp, value);
        }

        private string _lenghtPgp;
        public string LenghtPgp
        {
            get => _lenghtPgp;
            set => Set(ref _lenghtPgp, value);
        }

        private string _widthPgp;
        public string WidthPgp
        {
            get => _widthPgp;
            set => Set(ref _widthPgp, value);
        }

        private string _heightPgp;
        public string HeightPgp
        {
            get => _heightPgp; 
            set => Set(ref _heightPgp, value);
        }
        #endregion

        #region TurbineMdp

        private string _weightMdp;
        public string WeightMdp
        {
            get => _weightMdp;
            set => Set(ref _weightMdp, value);
        }

        private string _lenghtMdp;
        public string LenghtMdp
        {
            get => _lenghtMdp;
            set => Set(ref _lenghtMdp, value);
        }

        private string _widthMdp;
        public string WidthMdp
        {
            get => _widthMdp;
            set => Set(ref _widthMdp, value);
        }

        private string _heightMdp;
        public string HeightMdp
        {
            get => _heightMdp;
            set => Set(ref _heightMdp, value);
        }
        #endregion

        #region TurbineImage
        private string _imagePathOne;
        public string ImagePathOne
        {
            get => _imagePathOne;
            set => Set(ref _imagePathOne, value);
        }

        private string _imagePathTwo;
        public string ImagePathTwo
        {
            get => _imagePathTwo;
            set => Set(ref _imagePathTwo, value);
        }

        private string _imagePathThree;
        public string ImagePathThree
        {
            get => _imagePathThree;
            set => Set(ref _imagePathThree, value);
        }

        private string _imagePathFour;
    
        public string ImagePathFour
        {
            get => _imagePathFour;
            set => Set(ref _imagePathFour, value);
        }
     
        #endregion

        #endregion

        public CreateOrUpdateTurbineVM() 
        {
            if (MainWindowViewModel.main.UpdTurbine != null)
            {
                ContentButton = "Изменить";
                OriginalName = MainWindowViewModel.main.UpdTurbine.TurbineName;
                TurbineNames = MainWindowViewModel.main.UpdTurbine.TurbineName;
                TurbineDescriptions = MainWindowViewModel.main.UpdTurbine.TurbineDescription;
                #region Update TurbineScpg
                PowerOutputScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.PowerOutput;
                FuelScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.Fuel;
                FrequencyScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.Frequency;
                GrossEfficiencyScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.GrossEfficiency;
                HeatRateScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.HeatRate;
                TurbineSpeedScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.TurbineSpeed;
                PressureRatioScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.PressureRatio;
                ExhaustMassFlowScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.ExhaustMassFlow;
                ExhaustTemperatureScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.ExhaustTemperature;
                EmmisionScpg = MainWindowViewModel.main.UpdTurbine.TurbineScpgNavigation.Emissions;
                #endregion
                #region Update TurbineMda
                PowerOutputMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.PowerOutput;
                FuelMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.Fuel;
                EfficiencyMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.Efficiency;
                HeatRateMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.HeatRate;
                DriveShaftSpeedMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.DriveShaftSpeed;
                PressureRatioMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.PressureRatio;
                ExhaustMassFlowMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.ExhaustMassFlow;
                ExhaustTemperatureMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.ExhaustTemperature;
                EmmisionMda = MainWindowViewModel.main.UpdTurbine.TurbineMdaNavigation.Emissions;
                #endregion
                #region Update TurbinePgp
                WeightPgp = MainWindowViewModel.main.UpdTurbine.TurbinePgpNavigation.Weight;
                LenghtPgp = MainWindowViewModel.main.UpdTurbine.TurbinePgpNavigation.Lenght;
                WidthPgp = MainWindowViewModel.main.UpdTurbine.TurbinePgpNavigation.Width;
                HeightPgp = MainWindowViewModel.main.UpdTurbine.TurbinePgpNavigation.Height;
                #endregion
                #region Update TurbineMdp
                WeightMdp = MainWindowViewModel.main.UpdTurbine.TurbineMdpNavigation.Weight;
                LenghtMdp = MainWindowViewModel.main.UpdTurbine.TurbineMdpNavigation.Lenght;
                WidthMdp = MainWindowViewModel.main.UpdTurbine.TurbineMdpNavigation.Width;
                HeightMdp = MainWindowViewModel.main.UpdTurbine.TurbineMdpNavigation.Height;
                #endregion
                #region Update TurbineImage
                ImagePathOne = pathProject + MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageOne;
                ImagePathTwo = pathProject + MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageTwo;
                ImagePathThree = pathProject + MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageThree;
                ImagePathFour = pathProject + MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageFour;
                #endregion
            }
            else 
            { 
                ContentButton = "Добавить";
                ImagePathOne = "/ResourceApp/Image/picture.png";
                ImagePathTwo = "/ResourceApp/Image/picture.png";
                ImagePathThree = "/ResourceApp/Image/picture.png";
                ImagePathFour = "/ResourceApp/Image/picture.png";

            }

            #region Command
            CreateOrUpdate = new LambdaCommand(OnCreateOrUpdateExecute, CanCreateOrUpdateExecute);

            OpenImage = new LambdaCommand(OnOpenImageExecute, CanOpenImageExecute);
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


        private void ValidateDecimalNumericPowerOutput()
        {
            bool stringIsEmpty = false;
            ClearErrors(nameof(PowerOutputScpg));
            if (string.IsNullOrWhiteSpace(PowerOutputScpg))
            {
                ClearErrors(nameof(PowerOutputScpg));
                AddError(nameof(PowerOutputScpg), "*Поле не может быть пустым.");
                stringIsEmpty = true;
            }
            if (!stringIsEmpty)
            {
                if (!Regex.IsMatch(PowerOutputScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(PowerOutputScpg), "*Ошибка валидации введите число или число с плавающей точкой");
                    
                }
            }   
            
                
        }

        private void ValidateDecimalNumericPowerOutputMda()
        {
            bool stringIsEmpty = false;
            ClearErrors(nameof(PowerOutputMda));
            if (string.IsNullOrWhiteSpace(PowerOutputMda))
            {
                ClearErrors(nameof(PowerOutputMda));
                AddError(nameof(PowerOutputMda), "*Поле не может быть пустым.");
                stringIsEmpty = true;
            }
            if (!stringIsEmpty)
            {
                if (!Regex.IsMatch(PowerOutputMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(PowerOutputMda), "*Ошибка валидации введите число или число с плавающей точкой");

                }
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
    }
}
