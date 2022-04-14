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
            APP_INSIGHTS.ai.TrackEvent("User maintenance - Close");
            _viewModel.Close();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("User maintenance - Load users");
                _viewModel.LoadUsers();
            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("User maintenance - Load users failed", ex);
                throw;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("User maintenance - Begin add");
            _viewModel.BeginEdit(true);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("User maintenance - Begin edit");
            _viewModel.BeginEdit(false);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("User maintenance - Delete");
                listControl.DeleteUser();
            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("User maintenance - Delete failed", ex);

                throw;
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("User maintenance - Undo");
            _viewModel.CancelEdit();
        }

        private void GetControlsList(Visual c)
        {
            int controlsCount = VisualTreeHelper.GetChildrenCount(c);

            for (int x = 0; x < controlsCount; x++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(c, x);

                if (v.GetType().Name == "TextBox")
                {
                    TextBox tb = (TextBox)v;
                    tb.Focus();
                }
                if (VisualTreeHelper.GetChildrenCount(v) > 0)
                {
                    GetControlsList(v);
                }

            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                APP_INSIGHTS.ai.TrackEvent("User maintenance - Save");
                GetControlsList(detailControl);
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
