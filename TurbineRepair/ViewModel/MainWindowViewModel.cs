using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TurbineRepair.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        private AutheticationVM autheticationVM { get; set; }

        #region Property

        #region CurrentControl
        private object _currentControl;

        public object CurrentControl 
        { 
            get => _currentControl;
            set => Set(ref _currentControl, value);
        }
        #endregion

        #region SizeWindows
        private object _sizeWindows;
        public object SizeWindows
        {
            get => _sizeWindows;
            set => Set(ref _sizeWindows, value);
        }
        #endregion

        #region Height and Width Windows

        private double _heightWin = 500;
        public double HeightWin
        {
            get => (int)_heightWin;
            set => Set(ref _heightWin, value);
        }

        private double _widthWin = 500;
        public double WidthWin
        {
            get => (int)_widthWin;
            set => Set(ref _widthWin, value);
        }
        #endregion

        #endregion


        public MainWindowViewModel() 
        {
           
            autheticationVM = new AutheticationVM();
            CurrentControl = autheticationVM;
            SizeWindows = new RectangleGeometry().Rect = new Rect(0,0, _widthWin,_heightWin);        
        }



    }
}
