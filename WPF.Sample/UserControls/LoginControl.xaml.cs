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

// 09/23/2020 02:29 am - SSN - [20200923-0216] - [002] - M03-06 - Create the login view model class 

namespace WPF.Sample.UserControls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {

        private LoginViewModel _viewModel = null;

        public LoginControl()
        {
            InitializeComponent();
            _viewModel = (LoginViewModel)this.Resources["viewModel"];
        }

        // 09/23/2020 05:25 am - SSN - [20200923-0428] - [002] - M04-06 - Create informational messages that timeout

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("User maintenance - Login");

                _viewModel.Entity.Password = txtPassword.Password;

                _viewModel.Login();
            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("User maintenance - Login failed", ex);

                throw;
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        { 
            APP_INSIGHTS.ai.TrackEvent("User maintenance - Login cancel");
            _viewModel.Close();
        }

    }
}
