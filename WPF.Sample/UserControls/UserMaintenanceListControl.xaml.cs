using System;
using System.Windows;
using System.Windows.Controls;
using WPF.Sample.DataLayer;
using WPF.Sample.ViewModelLayer;
using Common.Library;


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
            APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0656: User maintenance - Begin edit");

            getBoundRecord(sender);
            _viewModel.BeginEdit(false);
        }

        private void getBoundRecord(object sender)
        {
            _viewModel.Entity = (User)((Button)sender).Tag;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0655: User maintenance - Delete");
                getBoundRecord(sender);
                DeleteUser();

            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("ps-253-20220415-0654: User maintenance - Delete failed", ex);

                throw;
            }
        }

        public void DeleteUser()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(MainWindow))
                {
                    MainWindow mw = (MainWindow)win;
                    if (mw.LoggedInUser.UserId == _viewModel.Entity.UserId)
                    {

                        MessageBox.Show("You can't delete your own record!" + Environment.NewLine +
                                        "Invalid Action", "Invalid Action", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }


            if (MessageBox.Show("Do you have access to the database?" + Environment.NewLine +
                "You cannot set passwords", "Confirm access to database", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0653: User maintenance - User deleting own record. ");
                return;
            }

            if (MessageBox.Show("Delete user " + _viewModel.Entity.LastName + ", " + _viewModel.Entity.FirstName + "?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _viewModel.Delete();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0652: User maintenance - Unload control");
            _viewModel = (UserMaintenanceViewModel)this.DataContext;
        }
    }
}
