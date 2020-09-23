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
using WPF.Sample.ViewModelLayer;

// 09/23/2020 04:23 am - SSN - [20200923-0404] - [004] - M04-04-Demo-Create-use-feedback-view-model (and UserMaintenance)

namespace WPF.Sample.UserControls
{
    /// <summary>
    /// Interaction logic for UserMaintenanceControl.xaml
    /// </summary>
    public partial class UserMaintenanceControl : UserControl
    {

        private UserMaintenanceViewModel _viewModel;

        public UserMaintenanceControl()
        {
            InitializeComponent();
            _viewModel = (UserMaintenanceViewModel)this.Resources["viewModel"];
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Close();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadUsers();
        }
    }
}
