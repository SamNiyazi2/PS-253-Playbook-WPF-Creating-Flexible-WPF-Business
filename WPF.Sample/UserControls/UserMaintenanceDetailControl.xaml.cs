﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPF.Sample.UserControls
{
    /// <summary>
    /// Interaction logic for UserMaintenanceDetailControl.xaml
    /// </summary>
    public partial class UserMaintenanceDetailControl : UserControl
    {
        public UserMaintenanceDetailControl()
        {
            InitializeComponent();
        }

        private UserMaintenanceViewModel _viewModel;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // 09/23/2020 02:02 pm - SSN - [20200923-1400] - [001] - M08-09 - Demo: Add button click events to change state 

            _viewModel = (UserMaintenanceViewModel)this.DataContext;
            IsEnabledChanged += UserMaintenanceDetailControl_IsEnabledChanged;
            

        }
        Timer timer;
    

        private void UserMaintenanceDetailControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            timer = new Timer( new TimerCallback( setFocus2));
            timer.Change(600, 0);
        }

        
        private void setFocus2(object state)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                username.Focus();
            });
        }

      

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("User maintenance - Undo");

            _viewModel.CancelEdit();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("User maintenance - Save");
                _viewModel.Save();

            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("User maintenance - Save failed", ex);

                throw;
            }
        }
    }
}
