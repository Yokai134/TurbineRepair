﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TurbineRepair.Infrastructure.Base;

namespace TurbineRepair.ViewModel
{
    internal class OpenProjectCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            try
            {
                if (ProjectVM.projectVM.SelectedProject != null)
                {
                    MainWindowViewModel.main.CurrentProject = ProjectVM.projectVM.SelectedProject;
                    MainVM.mainVM.MainCurrentControl = new ProjectContentVM();
                }
                else MessageBox.Show("Не выбран проект", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch { }
        }    

            
    }
}
