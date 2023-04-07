﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls;

namespace TurbineRepair.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public static MainWindowViewModel main;
        #region UserControl
        private AutheticationVM autheticationVM { get; set; }
        private PinCodeVM pinCodeVM { get; set; }
        #endregion

        #region Property

        #region CurrentControl
        private object _currentControl;

        public object CurrentControl 
        { 
            get => _currentControl;
            set => Set(ref _currentControl, value);
        }
        #endregion      

        #endregion


        public MainWindowViewModel() 
        {           
            CurrentControl = new AutheticationVM();
            main = this;     
        }




    }
}
