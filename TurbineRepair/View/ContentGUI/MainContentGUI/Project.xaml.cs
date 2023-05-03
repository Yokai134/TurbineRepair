﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TurbineRepair.ViewModel;

namespace TurbineRepair.View.ContentGUI.MainContentGUI
{
    /// <summary>
    /// Логика взаимодействия для Project.xaml
    /// </summary>
    public partial class Project : UserControl
    {
        public Project()
        {
            InitializeComponent();
            for (int i = 0; i < MainWindowViewModel.main.UsersAll.Count; i++)
            {
                cb_users.Items.Add(MainWindowViewModel.main.UsersAll[i].Surname + " " +
                    MainWindowViewModel.main.UsersAll[i].Name + " " + MainWindowViewModel.main.UsersAll[i].Patronomyc);
            }
        }


    }
}
