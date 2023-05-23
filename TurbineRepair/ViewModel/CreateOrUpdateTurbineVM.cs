using Accessibility;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Win32;
using ScottPlot;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateTurbineVM : Base.ViewModel, INotifyDataErrorInfo
    {
        private string pathProject = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\..\\..\\" + @"TurbineResource");
        #region Regex
        string decimalNumber = @"^\d+([,]\d+?)?$";
        string freqencyNumber = @"^\d+([\/]\d+?)?$";
        string pressureRation = @"\d+([,]\d+?)+([:]?\d+)?$";
        string driveshaftPattern = @"\d+([,]\d+?)+([-]?\d+)?$";
        string patternLetter = @"^[а-яА-Яa-zA-z(),-.0-9\s\n]+$";
        string patternNames = @"^[a-zA-Z-0-9]+$";
        #endregion


        #region Property

        private string _picturePath = "/ResourceApp/Image/picture.png";

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

        private int _countImage = 0;
        public int CountImage
        {
            get => _countImage;
            set => Set(ref _countImage, value);
        }

        private byte[] _imagePath;
        public byte[] ImagePath
        {
            get => _imagePath;
            set => Set(ref _imagePath, value);
        }

        private string _imagePathFile;
        public string ImagePathFile
        {
            get => _imagePathFile;
            set => Set(ref _imagePathFile, value);
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

        private string _imageSourceOne;
        private string _imageSourceTwo;
        private string _imageSourceThree;
        private string _imageSourceFour;

        #region Turbine Field

        #region Turbine Main

        private string _turbineNameValid;
        public string TurbineNamesValid
        {
            get => _turbineNameValid;
            set
            {
                Set(ref _turbineNameValid, value);
                ValidateTurbineName();
            }
        }

        private string _turbineDescriptions;
        public string TurbineDescriptions
        {
            get => _turbineDescriptions;
            set
            {
                Set(ref _turbineDescriptions, value);
                ValidateDescription();
            }
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
                ValidateDecimalNumericPowerOutputSCPG();
            }
        }

        private string _fuelScpg;
        public string FuelScpg
        {
            get => _fuelScpg;
            set
            {
                Set(ref _fuelScpg, value);
                ValidateFuelSCPG();
            }
        }

        private string _frequencyScpg;
        public string FrequencyScpg
        {
            get => _frequencyScpg;
            set
            {
                Set(ref _frequencyScpg, value);
                ValidateFreqency();
            }
        }

        private string _grossEfficiencyScpg;
        public string GrossEfficiencyScpg
        {
            get => _grossEfficiencyScpg;
            set
            {
                Set(ref _grossEfficiencyScpg, value);
                ValidateGrossEfficiency();
            }
        }

        private string _heatRateScpg;
        public string HeatRateScpg
        {
            get => _heatRateScpg;
            set
            {
                Set(ref _heatRateScpg, value);
                ValidateHeatRateSCPG();
            }
        }

        private string _turbineSpeedScpg;
        public string TurbineSpeedScpg
        {
            get => _turbineSpeedScpg;
            set
            {
                Set(ref _turbineSpeedScpg, value);
                ValidateTurbineSpeed();
            }
        }

        private string _pressureRatioScpg;
        public string PressureRatioScpg
        {
            get => _pressureRatioScpg;
            set
            {
                Set(ref _pressureRatioScpg, value);
                ValidatePressureRationSCPG();
            }

        }

        private string _exhaustMassFlowScpg;
        public string ExhaustMassFlowScpg
        {
            get => _exhaustMassFlowScpg;
            set
            {
                Set(ref _exhaustMassFlowScpg, value);
                ValidateExhaustMassFlowSCPG();
            }
        }

        private string _exhaustTemperatureScpg;
        public string ExhaustTemperatureScpg
        {
            get => _exhaustTemperatureScpg;
            set
            {
                Set(ref _exhaustTemperatureScpg, value);
                ValidateExhaustTempSCPG();
            }
        }

        private string _emmisionScpg;
        public string EmmisionScpg
        {
            get => _emmisionScpg;
            set
            {
                Set(ref _emmisionScpg, value);
                ValidateEmissionsSCPG();
            }
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
                ValidateDecimalNumericPowerOutputMDA();

            }
        }

        private string _fuelMda;
        public string FuelMda
        {
            get => _fuelMda;
            set
            {
                Set(ref _fuelMda, value);
                ValidateFuelMDA();
            }
        }

        private string _efficiencyMda;
        public string EfficiencyMda
        {
            get => _efficiencyMda;
            set
            {
                Set(ref _efficiencyMda, value);
                ValidateEfficiencyMDA();
            }
        }

        private string _heatRateMda;
        public string HeatRateMda
        {
            get => _heatRateMda;
            set
            {
                Set(ref _heatRateMda, value);
                ValidateHeatRateMDA();
            }
        }

        private string _driveShaftSpeedMda;
        public string DriveShaftSpeedMda
        {
            get => _driveShaftSpeedMda;
            set
            {
                Set(ref _driveShaftSpeedMda, value);
                ValidateDriveShaftSpeedMDA();
            }
        }

        private string _pressureRatioMda;
        public string PressureRatioMda
        {
            get => _pressureRatioMda;
            set
            {
                Set(ref _pressureRatioMda, value);
                ValidatePressureRationMDA();
            }
        }

        private string _exhaustMassFlowMda;
        public string ExhaustMassFlowMda
        {
            get => _exhaustMassFlowMda;
            set
            {
                Set(ref _exhaustMassFlowMda, value);
                ValidateExhaustMassFlowMDA();
            }
        }

        private string _exhaustTemperatureMda;
        public string ExhaustTemperatureMda
        {
            get => _exhaustTemperatureMda;
            set
            {
                Set(ref _exhaustTemperatureMda, value);
                ValidateExhaustTempMDA();
            }
        }

        private string _emmisionMda;
        public string EmmisionMda
        {
            get => _emmisionMda;
            set
            {
                Set(ref _emmisionMda, value);
                ValidateEmissionsMDA();
            }
        }

        #endregion

        #region TurbinePgp

        private string _weightPgp;
        public string WeightPgp
        {
            get => _weightPgp;
            set
            {
                Set(ref _weightPgp, value);
                ValidateWeightPGP();
            }
        }

        private string _lenghtPgp;
        public string LenghtPgp
        {
            get => _lenghtPgp;
            set
            {
                Set(ref _lenghtPgp, value);
                ValidateLenghtPGP();
            }
        }

        private string _widthPgp;
        public string WidthPgp
        {
            get => _widthPgp;
            set
            {
                Set(ref _widthPgp, value);
                ValidateWidthPGP();
            }
        }

        private string _heightPgp;
        public string HeightPgp
        {
            get => _heightPgp;
            set
            {
                Set(ref _heightPgp, value);
                ValidateHeightPGP();
            }
        }
        #endregion

        #region TurbineMdp

        private string _weightMdp;
        public string WeightMdp
        {
            get => _weightMdp;
            set
            {
                Set(ref _weightMdp, value);
                ValidateWeightMDP();
            }
        }

        private string _lenghtMdp;
        public string LenghtMdp
        {
            get => _lenghtMdp;
            set
            {
                Set(ref _lenghtMdp, value);
                ValidateLenghtMDP();
            }
        }

        private string _widthMdp;
        public string WidthMdp
        {
            get => _widthMdp;
            set
            {
                Set(ref _widthMdp, value);
                ValidateWidthMDP();
            }
        }

        private string _heightMdp;
        public string HeightMdp
        {
            get => _heightMdp;
            set
            {
                Set(ref _heightMdp, value);
                ValidateHeightMDP();
            }
        }
        #endregion

        #region TurbineImage
        private byte[] _imagePathOne;
        public byte[] ImagePathOne
        {
            get => _imagePathOne;
            set
            {
                Set(ref _imagePathOne, value);
                ValidateImageOne();
            }
        }

        private byte[] _imagePathTwo;
        public byte[] ImagePathTwo
        {
            get => _imagePathTwo;
            set
            {
                Set(ref _imagePathTwo, value);
                ValidateImageTwo();
            }
        }

        private byte[] _imagePathThree;
        public byte[] ImagePathThree
        {
            get => _imagePathThree;
            set
            {
                Set(ref _imagePathThree, value);
                ValidateImageThree();
            }
        }

        private byte[] _imagePathFour;
        public byte[] ImagePathFour
        {
            get => _imagePathFour;
            set
            {
                Set(ref _imagePathFour, value);
                ValidateImageFour();
            }
        }

        #endregion

        #endregion

        #region BoolValidate

        #region BoolTurbineMain
        private bool _checkTurbineName;

        private bool _checkTurbineDescription;

        #endregion

        #region BoolTurbineSCPG
        private bool _checkPowerOutputScpg;

        private bool _checkFuelScpg;
  
        private bool _checkFrequency;
 
        private bool _checkGrossEfficiency;

        private bool _checkHeatRateScpg;

        private bool _checkTurbineSpeedScpg;

        private bool _checkExhaustTemperatureScpg;
 
        private bool _checkPressureRatioScpg;
 
        private bool _checkExhaustMassFlowScpg;

        private bool _checkEmissionScpg;

        #endregion

        #region BoolTurbineMDA
        private bool _checkPowerOutputMda;

        private bool _checkFuelMda;

        private bool _checkEfficiency;

        private bool _checkHeatRateMda;

        private bool _checkDriveShaftSpeed;

        private bool _checkExhaustTemperatureMda;

        private bool _checkPressureRatioMda;

        private bool _checkExhaustMassFlowMda;

        private bool _checkEmissionMda;

        #endregion

        #region BoolTurbinePGP
        private bool _checkWeightPgp;
        private bool _checkLenghtPgp;
        private bool _checkWidthPgp;
        private bool _checkHeightPgp;
        #endregion

        #region BoolTurbineMDP
        private bool _checkWeightMdp;
        private bool _checkLenghtMdp;
        private bool _checkWidthMdp;
        private bool _checkHeightMdp;
        #endregion

        #region BoolTurbineImage
        private bool _checkImageOne;
        private bool _checkImageTwo;
        private bool _checkImageThree;
        private bool _checkImageFour;
        #endregion

        #endregion


        #endregion

        #region Command


        public ICommand CreateOrUpdate { get; }
        private bool CanCreateOrUpdateExecute(object parameter) => true;
        private async void OnCreateOrUpdateExecute(object parameter)
        {

            
                #region ValidatePre
                ValidateTurbineName();
                ValidateDescription();
                #region ValidatePreSCPG
                ValidateDecimalNumericPowerOutputSCPG();
                ValidateFuelSCPG();
                ValidateFreqency();
                ValidateHeatRateSCPG();
                ValidateGrossEfficiency();
                ValidateTurbineSpeed();
                ValidatePressureRationSCPG();
                ValidateExhaustMassFlowSCPG();
                ValidateExhaustTempSCPG();
                ValidateEmissionsSCPG();
                #endregion
                #region ValidatePreMDA
                ValidateDecimalNumericPowerOutputMDA();
                ValidateFuelMDA();
                ValidateHeatRateMDA();
                ValidateEfficiencyMDA();
                ValidateDriveShaftSpeedMDA();
                ValidatePressureRationMDA();
                ValidateExhaustMassFlowMDA();
                ValidateExhaustTempMDA();
                ValidateEmissionsMDA();
                #endregion
                #region ValidatePrePGP
                ValidateWeightPGP();
                ValidateLenghtPGP();
                ValidateWidthPGP();
                ValidateHeightPGP();
                #endregion
                #region ValidatePreMDP
                ValidateWeightMDP();
                ValidateHeightMDP();
                ValidateWidthMDP();
                ValidateLenghtMDP();
                #endregion
                #region ValidateImege
                ValidateImageOne();
                ValidateImageTwo();
                ValidateImageThree();
                ValidateImageFour();
            #endregion
            #endregion
            if (MainWindowViewModel.main.UpdTurbine != null)
            {
                if (CheckName(TurbineNamesValid) || TurbineNamesValid == OriginalName)
                    if (_checkPowerOutputScpg && _checkFuelScpg && _checkFrequency && _checkHeatRateScpg && _checkGrossEfficiency && _checkTurbineSpeedScpg
                        && _checkPressureRatioScpg && _checkExhaustMassFlowScpg && _checkExhaustTemperatureScpg && _checkEmissionScpg &&
                        _checkPowerOutputMda && _checkFuelMda && _checkHeatRateMda && _checkEfficiency && _checkDriveShaftSpeed
                        && _checkPressureRatioMda && _checkExhaustMassFlowMda && _checkExhaustTemperatureMda && _checkEmissionMda &&
                        _checkWeightPgp && _checkLenghtPgp && _checkWidthPgp && _checkHeightPgp && _checkWeightMdp && _checkLenghtMdp && _checkWidthMdp && _checkHeightMdp
                        && _checkTurbineDescription && _checkTurbineName && _checkImageOne && _checkImageTwo && _checkImageThree && _checkImageFour)
                    {
                        Turbine updTurbine = MainWindowViewModel.main.UpdTurbine;
                        updTurbine.TurbineName = TurbineNamesValid;
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
                        updTurbine.TurbineImageNavigation.ImageOne = ImagePathOne;
                        updTurbine.TurbineImageNavigation.ImageTwo = ImagePathTwo;
                        updTurbine.TurbineImageNavigation.ImageThree = ImagePathThree;
                        updTurbine.TurbineImageNavigation.ImageFour = ImagePathFour;
                        #endregion
                        MainWindowViewModel.context.SaveChanges();
                        await MainWindowViewModel.main.UpdateData();
                        FailedAddOrUpdate = "*Данные обновлены";
                        ForegroundFailedMessage = 1;
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += OpenTurbineList;
                        timer.Start();
                    }
                    else
                    {
                        FailedAddOrUpdate = "*Данные не удалось обновить";
                        ForegroundFailedMessage = -1;
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += RefreshValidator;
                        timer.Start();
                    }
                else
                {
                    FailedAddOrUpdate = "*Данное наименование уже используется";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
            else
            {
                if (CheckName(TurbineNamesValid))
                {


                    if (_checkPowerOutputScpg && _checkFuelScpg && _checkFrequency && _checkHeatRateScpg && _checkGrossEfficiency && _checkTurbineSpeedScpg
                          && _checkPressureRatioScpg && _checkExhaustMassFlowScpg && _checkExhaustTemperatureScpg && _checkEmissionScpg &&
                          _checkPowerOutputMda && _checkFuelMda && _checkHeatRateMda && _checkEfficiency && _checkDriveShaftSpeed
                          && _checkPressureRatioMda && _checkExhaustMassFlowMda && _checkExhaustTemperatureMda && _checkEmissionMda &&
                          _checkWeightPgp && _checkLenghtPgp && _checkWidthPgp && _checkHeightPgp && _checkWeightMdp && _checkLenghtMdp && _checkWidthMdp && _checkHeightMdp
                          && _checkTurbineDescription && _checkTurbineName && _checkImageOne && _checkImageTwo && _checkImageThree && _checkImageFour)
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
                            TurbineName = TurbineNamesValid,
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
                        FailedAddOrUpdate = "*Турбина добавленна ";
                        ForegroundFailedMessage = 1;
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += RefreshContent;
                        timer.Start();
                    }
                    else
                    {
                        FailedAddOrUpdate = "*Данные не удалось добавить";
                        ForegroundFailedMessage = -1;
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += RefreshValidator;
                        timer.Start();
                    }

                }
                else
                {

                }
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
            ImagePathFile = openFileDialog.FileName;
            ImagePath = File.ReadAllBytes(openFileDialog.FileName);
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

        public ICommand BackTurbineList { get; }
        private bool CanBackTurbineListExecute(object parameter) => true;
        private async void OnBackTubineListExecute(object parametr) 
        {
            await MainWindowViewModel.main.UpdateData();
            MainVM.mainVM.MainCurrentControl = new TurbineVM();
            
        }
  
        #endregion

    

        public CreateOrUpdateTurbineVM() 
        {
            if (MainWindowViewModel.main.UpdTurbine != null)
            {
                ContentButton = "Изменить";
                OriginalName = MainWindowViewModel.main.UpdTurbine.TurbineName;
                TurbineNamesValid = MainWindowViewModel.main.UpdTurbine.TurbineName;
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
                ImagePathOne = MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageOne;
                ImagePathTwo = MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageTwo;
                ImagePathThree = MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageThree;
                ImagePathFour = MainWindowViewModel.main.UpdTurbine.TurbineImageNavigation.ImageFour;
                #endregion
            }
            else 
            { 
                ContentButton = "Добавить";              

            }

            #region Command
            CreateOrUpdate = new LambdaCommand(OnCreateOrUpdateExecute, CanCreateOrUpdateExecute);
            BackTurbineList = new LambdaCommand(OnBackTubineListExecute, CanBackTurbineListExecute);
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

        private void ValidateDescription()
        {
            ClearErrors(nameof(TurbineDescriptions));
            if (string.IsNullOrWhiteSpace(TurbineDescriptions) || !Regex.IsMatch(TurbineDescriptions, patternLetter, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(TurbineDescriptions));
                AddError(nameof(TurbineDescriptions), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                _checkTurbineDescription = false;
            }
            else
            {
                _checkTurbineDescription = true;
            }
        }

        private void ValidateTurbineName()
        {
            ClearErrors(nameof(TurbineNamesValid));
            if (string.IsNullOrWhiteSpace(TurbineNamesValid))
            {
                ClearErrors(nameof(TurbineNamesValid));
                AddError(nameof(TurbineNamesValid), "*Поле не может быть пустым.");
                _checkTurbineName = false;

            }
            else
            {
                if (!Regex.IsMatch(TurbineNamesValid, patternNames, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(TurbineNamesValid), "*Недопустимые символы");
                    _checkTurbineName = false;
                }
                else _checkTurbineName = true;                           
            }
        }

        #region Validate SCPG
        private void ValidateDecimalNumericPowerOutputSCPG()
        {

            ClearErrors(nameof(PowerOutputScpg));
            if (string.IsNullOrWhiteSpace(PowerOutputScpg))
            {
                ClearErrors(nameof(PowerOutputScpg));
                AddError(nameof(PowerOutputScpg), "*Поле не может быть пустым.");
                _checkPowerOutputScpg = false;

            }
            else
            {
                if (!Regex.IsMatch(PowerOutputScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(PowerOutputScpg), "*Введите число или число с плавающей точкой");
                    _checkPowerOutputScpg = false;
                }
                else _checkPowerOutputScpg = true;
            }
                           
        }

        private void ValidateFuelSCPG()
        {
            ClearErrors(nameof(FuelScpg));
            if (string.IsNullOrWhiteSpace(FuelScpg) || !Regex.IsMatch(FuelScpg, patternLetter, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(FuelScpg));
                AddError(nameof(FuelScpg), "*Поле не может быть пустым.");
                _checkFuelScpg = false;
            }
            else _checkFuelScpg = true;
        }

        private void ValidateFreqency()
        {
            ClearErrors(nameof(FrequencyScpg));
            if (string.IsNullOrWhiteSpace(FrequencyScpg))
            {
                ClearErrors(nameof(FrequencyScpg));
                AddError(nameof(FrequencyScpg), "*Поле не может быть пустым.");
                _checkFrequency = false;
            }
            else
            {

                if (!Regex.IsMatch(FrequencyScpg, freqencyNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(FrequencyScpg), "*Введите число (можно испоьзовать символ / )");
                    _checkFrequency = false;
                }
                else _checkFrequency = true;
            }
        }

        private void ValidateGrossEfficiency()
        {

            ClearErrors(nameof(GrossEfficiencyScpg));
            if (string.IsNullOrWhiteSpace(GrossEfficiencyScpg))
            {
                ClearErrors(nameof(GrossEfficiencyScpg));
                AddError(nameof(GrossEfficiencyScpg), "*Поле не может быть пустым.");
                _checkGrossEfficiency = false;

            }
            else
            {
                if (!Regex.IsMatch(GrossEfficiencyScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(GrossEfficiencyScpg), "*Введите число или число с плавающей точкой");
                    _checkGrossEfficiency = false;
                }
                else _checkGrossEfficiency = true;
            }
        }

        private void ValidateHeatRateSCPG()
        {
            ClearErrors(nameof(HeatRateScpg));
            if (string.IsNullOrWhiteSpace(HeatRateScpg))
            {
                ClearErrors(nameof(HeatRateScpg));
                AddError(nameof(HeatRateScpg), "*Поле не может быть пустым.");
                _checkHeatRateScpg = false;
            }
            else
            {
                if (!Regex.IsMatch(HeatRateScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(HeatRateScpg), "*Введите число или число с плавающей точкой");
                    _checkHeatRateScpg = false;
                }
                else _checkHeatRateScpg = true;
            }
        }

        private void ValidateTurbineSpeed()
        {
            ClearErrors(nameof(TurbineSpeedScpg));
            if (string.IsNullOrWhiteSpace(TurbineSpeedScpg))
            {
                ClearErrors(nameof(TurbineSpeedScpg));
                AddError(nameof(TurbineSpeedScpg), "*Поле не может быть пустым.");
                _checkTurbineSpeedScpg = false;
            }
            else
            {
                if (!Regex.IsMatch(TurbineSpeedScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(TurbineSpeedScpg), "*Введите число или число с плавающей точкой");
                    _checkTurbineSpeedScpg = false;
                }
                else _checkTurbineSpeedScpg = true;
            }
        }

        private void ValidatePressureRationSCPG()
        {

            ClearErrors(nameof(PressureRatioScpg));
            if (string.IsNullOrWhiteSpace(PressureRatioScpg))
            {
                ClearErrors(nameof(PressureRatioScpg));
                AddError(nameof(PressureRatioScpg), "*Поле не может быть пустым.");
                _checkPressureRatioScpg = false;

            }
            else
            {
                if (!Regex.IsMatch(PressureRatioScpg, pressureRation, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(PressureRatioScpg), "*Введите число или число с плавающей точкой");
                    _checkPressureRatioScpg = false;
                }
                else _checkPressureRatioScpg = true;
            }
        }

        private void ValidateExhaustMassFlowSCPG()
        {
            ClearErrors(nameof(ExhaustMassFlowScpg));
            if (string.IsNullOrWhiteSpace(ExhaustMassFlowScpg))
            {
                ClearErrors(nameof(ExhaustMassFlowScpg));
                AddError(nameof(ExhaustMassFlowScpg), "*Поле не может быть пустым.");
                _checkExhaustMassFlowScpg = false;
                
            }
            else
            {
                if (!Regex.IsMatch(ExhaustMassFlowScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(ExhaustMassFlowScpg), "*Введите число или число с плавающей точкой");
                    _checkExhaustMassFlowScpg = false;

                }
                else _checkExhaustMassFlowScpg = true;
            }
        }

        private void ValidateExhaustTempSCPG()
        {

            ClearErrors(nameof(ExhaustTemperatureScpg));
            if (string.IsNullOrWhiteSpace(ExhaustTemperatureScpg))
            {
                ClearErrors(nameof(ExhaustTemperatureScpg));
                AddError(nameof(ExhaustTemperatureScpg), "*Поле не может быть пустым.");
                _checkExhaustTemperatureScpg = false;
            }
            else
            {
                if (!Regex.IsMatch(ExhaustTemperatureScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(ExhaustTemperatureScpg), "*Введите число или число с плавающей точкой");
                    _checkExhaustTemperatureScpg = false;
                }
                else _checkExhaustTemperatureScpg = true;
            }
        }

        private void ValidateEmissionsSCPG()
        {
            ClearErrors(nameof(EmmisionScpg));
            if (string.IsNullOrWhiteSpace(EmmisionScpg))
            {
                ClearErrors(nameof(EmmisionScpg));
                AddError(nameof(EmmisionScpg), "*Поле не может быть пустым.");
                _checkEmissionScpg = false;
            }
            else
            {
                if (!Regex.IsMatch(EmmisionScpg, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(EmmisionScpg), "*Введите число или число с плавающей точкой");
                    _checkEmissionScpg = false;
                }
                else _checkEmissionScpg = true;
            }
        }
        #endregion

        #region Validate MDA
        private void ValidateDecimalNumericPowerOutputMDA()
        {
            ClearErrors(nameof(PowerOutputMda));
            if (string.IsNullOrWhiteSpace(PowerOutputMda))
            {
                ClearErrors(nameof(PowerOutputMda));
                AddError(nameof(PowerOutputMda), "*Поле не может быть пустым.");
                _checkPowerOutputMda = false;
            }
            else
            {
                if (!Regex.IsMatch(PowerOutputMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(PowerOutputMda), "*Введите число или число с плавающей точкой");
                    _checkPowerOutputMda = false;
                }
                else _checkPowerOutputMda = true;
            }

        }

        private void ValidateFuelMDA()
        {
            ClearErrors(nameof(FuelMda));
            if (string.IsNullOrWhiteSpace(FuelMda) || !Regex.IsMatch(FuelMda, patternLetter, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(FuelMda));
                AddError(nameof(FuelMda), "*Поле не может быть пустым.");
                _checkFuelMda = false;
            }
            else _checkFuelMda = true;
        }

        private void ValidateEfficiencyMDA()
        {
            ClearErrors(nameof(EfficiencyMda));
            if (string.IsNullOrWhiteSpace(EfficiencyMda))
            {
                ClearErrors(nameof(EfficiencyMda));
                AddError(nameof(EfficiencyMda), "*Поле не может быть пустым.");
                _checkEfficiency = false;
            }
            else
            {
                if (!Regex.IsMatch(EfficiencyMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(EfficiencyMda), "*Введите число или число с плавающей точкой");
                    _checkEfficiency = false;
                }
                else _checkEfficiency = true;
            }
        }

        private void ValidateHeatRateMDA()
        {
            ClearErrors(nameof(HeatRateMda));
            if (string.IsNullOrWhiteSpace(HeatRateMda))
            {
                ClearErrors(nameof(HeatRateMda));
                AddError(nameof(HeatRateMda), "*Поле не может быть пустым.");
                _checkHeatRateMda = false;
            }
            else
            {
                if (!Regex.IsMatch(HeatRateMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(HeatRateMda), "*Введите число или число с плавающей точкой");
                    _checkHeatRateMda = false;
                }
                else _checkHeatRateMda = true;
            }
        }

        private void ValidateDriveShaftSpeedMDA()
        {
            ClearErrors(nameof(DriveShaftSpeedMda));
            if (string.IsNullOrWhiteSpace(DriveShaftSpeedMda))
            {
                ClearErrors(nameof(DriveShaftSpeedMda));
                AddError(nameof(DriveShaftSpeedMda), "*Поле не может быть пустым.");
                _checkDriveShaftSpeed = false;
            }
            else
            {
                if (!Regex.IsMatch(DriveShaftSpeedMda, driveshaftPattern, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(DriveShaftSpeedMda), "*Введите число или число с плавающей точкой");
                    _checkDriveShaftSpeed = false;
                }
                else _checkDriveShaftSpeed = true;
            }
        }

        private void ValidatePressureRationMDA()
        {
            ClearErrors(nameof(PressureRatioMda));
            if (string.IsNullOrWhiteSpace(PressureRatioMda))
            {
                ClearErrors(nameof(PressureRatioMda));
                AddError(nameof(PressureRatioMda), "*Поле не может быть пустым.");
                _checkPressureRatioMda = false;
            }
            else
            {
                if (!Regex.IsMatch(PressureRatioMda, pressureRation, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(PressureRatioMda), "*Введите число или число с плавающей точкой");
                    _checkPressureRatioMda = false;
                }
                else _checkPressureRatioMda = true;
            }
        }

        private void ValidateExhaustMassFlowMDA()
        {
            ClearErrors(nameof(ExhaustMassFlowMda));
            if (string.IsNullOrWhiteSpace(ExhaustMassFlowMda))
            {
                ClearErrors(nameof(ExhaustMassFlowMda));
                AddError(nameof(ExhaustMassFlowMda), "*Поле не может быть пустым.");
                _checkExhaustMassFlowMda = false;
            }
            else
            {
                if (!Regex.IsMatch(ExhaustMassFlowMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(ExhaustMassFlowMda), "*Введите число или число с плавающей точкой");
                    _checkExhaustMassFlowMda = false;
                }
                else _checkExhaustMassFlowMda = true;
            }
        }

        private void ValidateExhaustTempMDA()
        {
            ClearErrors(nameof(ExhaustTemperatureMda));
            if (string.IsNullOrWhiteSpace(ExhaustTemperatureMda))
            {
                ClearErrors(nameof(ExhaustTemperatureMda));
                AddError(nameof(ExhaustTemperatureMda), "*Поле не может быть пустым.");
                _checkExhaustTemperatureMda = false;

            }
            else
            {
                if (!Regex.IsMatch(ExhaustTemperatureMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(ExhaustTemperatureMda), "*Введите число или число с плавающей точкой");
                    _checkExhaustTemperatureMda = false;
                }
                else _checkExhaustTemperatureMda = true;
            }
        }

        private void ValidateEmissionsMDA()
        {
            ClearErrors(nameof(EmmisionMda));
            if (string.IsNullOrWhiteSpace(EmmisionMda))
            {
                ClearErrors(nameof(EmmisionMda));
                AddError(nameof(EmmisionMda), "*Поле не может быть пустым.");
                _checkEmissionMda = false;
            }
            else
            {
                if (!Regex.IsMatch(EmmisionMda, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(EmmisionMda), "*Введите число или число с плавающей точкой");
                    _checkEmissionMda = false;
                }
                else _checkEmissionMda = true;
            }
        }

        #endregion

        #region Validate PGP
        private void ValidateWeightPGP()
        {
            ClearErrors(nameof(WeightPgp));
            if (string.IsNullOrWhiteSpace(WeightPgp))
            {
                ClearErrors(nameof(WeightPgp));
                AddError(nameof(WeightPgp), "*Поле не может быть пустым.");
                _checkWeightPgp = false;
            }
            else
            {
                if (!Regex.IsMatch(WeightPgp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(WeightPgp), "*Введите число или число с плавающей точкой");
                    _checkWeightPgp = false;
                }
                else _checkWeightPgp = true;
            }

        }

        private void ValidateLenghtPGP()
        {

            ClearErrors(nameof(LenghtPgp));
            if (string.IsNullOrWhiteSpace(LenghtPgp))
            {
                ClearErrors(nameof(LenghtPgp));
                AddError(nameof(LenghtPgp), "*Поле не может быть пустым.");
                _checkLenghtPgp = false;
            }
            else
            {
                if (!Regex.IsMatch(LenghtPgp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(LenghtPgp), "*Введите число или число с плавающей точкой");
                    _checkLenghtPgp = false;
                }
                else _checkLenghtPgp = true;
            }

        }

        private void ValidateWidthPGP()
        {

            ClearErrors(nameof(WidthPgp));
            if (string.IsNullOrWhiteSpace(WidthPgp))
            {
                ClearErrors(nameof(WidthPgp));
                AddError(nameof(WidthPgp), "*Поле не может быть пустым.");
                _checkWidthPgp = false;

            }
            else
            {
                if (!Regex.IsMatch(WidthPgp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(WidthPgp), "*Введите число или число с плавающей точкой");
                    _checkWidthPgp = false;
                }
                else _checkWidthPgp = true;
            }

        }

        private void ValidateHeightPGP()
        {

            ClearErrors(nameof(HeightPgp));
            if (string.IsNullOrWhiteSpace(HeightPgp))
            {
                ClearErrors(nameof(HeightPgp));
                AddError(nameof(HeightPgp), "*Поле не может быть пустым.");
                _checkHeightPgp = false;
            }
            else
            {
                if (!Regex.IsMatch(HeightPgp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(HeightPgp), "*Введите число или число с плавающей точкой");
                    _checkHeightPgp = false;
                }
                else _checkHeightPgp = true;
            }

        }
        #endregion

        #region Validate PGP
        private void ValidateWeightMDP()
        {
         
            ClearErrors(nameof(WeightMdp));
            if (string.IsNullOrWhiteSpace(WeightMdp))
            {
                ClearErrors(nameof(WeightMdp));
                AddError(nameof(WeightMdp), "*Поле не может быть пустым.");
                _checkWeightMdp = false;

            }
            else
            {
                if (!Regex.IsMatch(WeightMdp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(WeightMdp), "*Введите число или число с плавающей точкой");
                    _checkWeightMdp = false;
                }
                else _checkWeightMdp = true;
            }

        }

        private void ValidateLenghtMDP()
        {
            ClearErrors(nameof(LenghtMdp));
            if (string.IsNullOrWhiteSpace(LenghtMdp))
            {
                ClearErrors(nameof(LenghtMdp));
                AddError(nameof(LenghtMdp), "*Поле не может быть пустым.");
                _checkLenghtMdp = false;
            }
            
            else
            {
                if (!Regex.IsMatch(LenghtMdp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(LenghtMdp), "*Введите число или число с плавающей точкой");
                    _checkLenghtMdp = false;
                }
                else _checkLenghtMdp = true;
            }

        }

        private void ValidateWidthMDP()
        {
            ClearErrors(nameof(WidthMdp));
            if (string.IsNullOrWhiteSpace(WidthMdp))
            {
                ClearErrors(nameof(WidthMdp));
                AddError(nameof(WidthMdp), "*Поле не может быть пустым.");
                _checkWidthMdp = false;
            }
            else
            {
                if (!Regex.IsMatch(WidthMdp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(WidthMdp), "*Введите число или число с плавающей точкой");
                    _checkWidthMdp = false;
                }
                else _checkWidthMdp = true;
            }

        }

        private void ValidateHeightMDP()
        {
            ClearErrors(nameof(HeightMdp));
            if (string.IsNullOrWhiteSpace(HeightMdp))
            {
                ClearErrors(nameof(HeightMdp));
                AddError(nameof(HeightMdp), "*Поле не может быть пустым.");
                _checkHeightMdp = false;
            }
            else
            {
                if (!Regex.IsMatch(HeightMdp, decimalNumber, RegexOptions.IgnoreCase))
                {
                    AddError(nameof(HeightMdp), "*Введите число или число с плавающей точкой");
                    _checkHeightMdp = false;
                }
                else _checkHeightMdp = true;
            }

        }
        #endregion

        #region Validate Image
        private void ValidateImageOne()
        {
            ClearErrors(nameof(ImagePathOne));
            if (ImagePathOne == null)
            {
                AddError(nameof(ImagePathOne), "*Не выбранно изображение");
                _checkImageOne = false;
            }
            else _checkImageOne = true;
        }
        private void ValidateImageTwo()
        {
            ClearErrors(nameof(ImagePathTwo));
            if (ImagePathTwo == null)
            {
                AddError(nameof(ImagePathTwo), "*Не выбранно изображение");
                _checkImageTwo = false;
            }
            else _checkImageTwo= true;
        }

        private void ValidateImageThree()
        {
            ClearErrors(nameof(ImagePathThree));
            if (ImagePathThree == null)
            {
                AddError(nameof(ImagePathThree), "*Не выбранно изображение");
                _checkImageThree = false;
            }
            else _checkImageThree = true;
        }
        private void ValidateImageFour()
        {
            ClearErrors(nameof(ImagePathFour));
            if (ImagePathFour == null)
            {
                AddError(nameof(ImagePathFour), "*Не выбранно изображение");
                _checkImageFour = false;
            }
            else _checkImageFour = true;
        }
        #endregion

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
            #region ClearErrors
            ClearErrors(nameof(TurbineNamesValid));
            ClearErrors(nameof(TurbineDescriptions));
            ClearErrors(nameof(PowerOutputScpg));
            ClearErrors(nameof(FuelScpg));  
            ClearErrors(nameof(HeatRateScpg));
            ClearErrors(nameof(GrossEfficiencyScpg));
            ClearErrors(nameof(TurbineSpeedScpg));
            ClearErrors(nameof(PressureRatioScpg));
            ClearErrors(nameof(ExhaustMassFlowScpg));
            ClearErrors(nameof(ExhaustTemperatureScpg));
            ClearErrors(nameof(EmmisionScpg));
            ClearErrors(nameof(PowerOutputMda));
            ClearErrors(nameof(FuelMda));
            ClearErrors(nameof(EfficiencyMda));
            ClearErrors(nameof(HeatRateMda));
            ClearErrors(nameof(PressureRatioMda));
            ClearErrors(nameof(DriveShaftSpeedMda));
            ClearErrors(nameof(ExhaustMassFlowMda));
            ClearErrors(nameof(ExhaustTemperatureMda));
            ClearErrors(nameof(EmmisionMda));
            ClearErrors(nameof(WeightPgp));
            ClearErrors(nameof(LenghtPgp));
            ClearErrors(nameof(WidthPgp));
            ClearErrors(nameof(HeightPgp));
            ClearErrors(nameof(WeightMdp));
            ClearErrors(nameof(LenghtMdp));
            ClearErrors(nameof(WidthMdp));
            ClearErrors(nameof(HeightMdp));
            ClearErrors(nameof(ImagePathOne));
            ClearErrors(nameof(ImagePathTwo));
            ClearErrors(nameof(ImagePathThree));
            ClearErrors(nameof(ImagePathFour));
            #endregion
            timer.Stop();
        }
        private void RefreshContent(object e, object sender)
        {
            #region Prop drop
            _checkTurbineName = false;
            TurbineNamesValid = string.Empty;
            _checkTurbineDescription = false;
            TurbineDescriptions = string.Empty;
            #region Drop SCPG
            _checkPowerOutputScpg = false;
            PowerOutputScpg = string.Empty;
            _checkFuelScpg = false;
            FuelScpg = string.Empty;
            _checkHeatRateScpg = false;
            HeatRateScpg = string.Empty;
            _checkGrossEfficiency = false;
            GrossEfficiencyScpg = string.Empty;
            _checkTurbineSpeedScpg = false;
            TurbineSpeedScpg = string.Empty;
            _checkPressureRatioScpg = false;
            PressureRatioScpg = string.Empty;
            _checkExhaustMassFlowMda = false;
            ExhaustMassFlowScpg = string.Empty;
            _checkExhaustTemperatureScpg = false;
            ExhaustTemperatureScpg = string.Empty;
            _checkEmissionScpg = false;
            EmmisionScpg = string.Empty;
            #endregion
            #region Drop MDA
            _checkPowerOutputMda = false;
            PowerOutputMda = string.Empty;
            _checkFuelMda = false;
            FuelMda = string.Empty;
            _checkHeatRateMda = false;
            HeatRateMda = string.Empty;
            _checkEfficiency = false;
            EfficiencyMda = string.Empty;
            _checkDriveShaftSpeed = false;
            DriveShaftSpeedMda = string.Empty;
            _checkPressureRatioMda = false;
            PressureRatioMda = string.Empty;
            _checkExhaustMassFlowMda = false;
            ExhaustMassFlowMda = string.Empty;
            _checkExhaustTemperatureMda = false;
            ExhaustTemperatureMda = string.Empty;
            _checkEmissionMda = false;
            EmmisionMda = string.Empty;
            #endregion
            #region Drop PGP
            _checkWeightPgp = false;
            WeightPgp = string.Empty;
            _checkLenghtPgp = false;
            LenghtPgp = string.Empty;
            _checkWidthPgp = false;
            WidthPgp = string.Empty;
            _checkHeightPgp = false;
            HeightPgp = string.Empty;
            #endregion
            #region Drop MDP
            _checkWeightMdp = false;
            WeightMdp = string.Empty;
            _checkLenghtMdp = false;
            LenghtMdp= string.Empty;
            _checkWidthMdp = false;
            WidthMdp = string.Empty;
            _checkHeightMdp = false;
            HeightMdp= string.Empty;
            #endregion
            #region DropImage
            ImagePathOne = null;
            ImagePathTwo = null;
            ImagePathThree = null;
            ImagePathFour = null;
            #endregion
            #endregion
            FailedAddOrUpdate = string.Empty;
            timer.Stop();
        }
        private void OpenTurbineList(object sender, object e)
        {
            MainVM.mainVM.MainCurrentControl = new TurbineVM();
            timer.Stop();
        }
        #endregion

        #region CheckName
        private bool CheckName(string name)
        {
            bool isCheked = true;
            for (int i = 0;i< MainWindowViewModel.main.TurbinesAll.Count;i++) 
            { 
                if(TurbineNamesValid == MainWindowViewModel.main.TurbinesAll[i].TurbineName)
                {
                    isCheked = false; 
                    break;
                }
            }
            return isCheked;
        }
        #endregion


        private async Task ImageMove(Turbine updTurbine)
        {
            if (OriginalName != updTurbine.TurbineName)
            {
                string basePath = Path.GetFullPath(pathProject + @"\" + OriginalName);
                string movePath = Path.GetFullPath(pathProject + @"\" + updTurbine.TurbineName);
                Directory.Move(basePath, movePath);
            }
            string imagePath = Path.GetFullPath(pathProject + @"\" + OriginalName);
            string[] imageFile = Directory.GetFiles(imagePath);
            if (ImageOneUpd)
            {
                string copyPath = imageFile[0];
                FileInfo fileInfo = new FileInfo(_imageSourceOne);
                using (FileStream fs = File.Open(copyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }

            }
            if (ImageTwoUpd)
            {

                string copyPath = imageFile[1];
                FileInfo fileInfo = new FileInfo(_imageSourceTwo);
                using (FileStream fs = File.Open(copyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
            }
            if (ImageThreeUpd)
            {

                string copyPath = imageFile[2];
                FileInfo fileInfo = new FileInfo(_imageSourceThree);
                using (FileStream fs = File.Open(copyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
            }
            if (ImageFourUpd)
            {

                string copyPath = imageFile[3];
                FileInfo fileInfo = new FileInfo(_imageSourceFour);
                using (FileStream fs = File.Open(copyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (fileInfo.Exists)
                    {
                        fileInfo.CopyTo(copyPath, true);
                    }
                }
            }

        }
    }
}
