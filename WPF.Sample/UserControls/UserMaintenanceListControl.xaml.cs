using System;
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
using WPF.Sample.DataLayer;
using WPF.Sample.ViewModelLayer;

namespace WPF.Sample.UserControls
{
    /// <summary>
    /// Interaction logic for UserMaintenanceListControl.xaml
    /// </summary>
    public partial class UserMaintenanceListControl : UserControl
    {
        public UserMaintenanceListControl()
        {
            InitializeComponent();
        }


        // 09/23/2020 02:13 pm - SSN - [20200923-1400] - [002] - M08-09 - Demo: Add button click events to change state 

        private UserMaintenanceViewModel _viewModel;



        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            getBoundRecord(sender);
            _viewModel.BeginEdit(false);
        }

        private void getBoundRecord(object sender)
        {
            _viewModel.Entity = (User)((Button)sender).Tag;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            getBoundRecord(sender);
            DeleteUser();
        }

        public void DeleteUser()
        {

            if (MessageBox.Show("Do you have access to the database?" + Environment.NewLine +
                "You cannot set passwords", "Confirm access to database", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            if (MessageBox.Show("Delete user " + _viewModel.Entity.LastName + ", " + _viewModel.Entity.FirstName + "?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _viewModel.Delete();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = (UserMaintenanceViewModel)this.DataContext;
        }
    }
}
