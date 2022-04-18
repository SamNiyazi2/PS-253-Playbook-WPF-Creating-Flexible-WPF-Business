using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF.Sample.ViewModelLayer;
using Common.Library;

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


        // 03/30/2022 05:49 pm - SSN - Set focus
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);


            // Sets the focused element in focusScope1
            // focusScope1 is a StackPanel.
            FocusManager.SetFocusedElement(mainGrid, UserName);
            txtPassword.Password = _viewModel.Entity.Password;

        }

        // 09/23/2020 05:25 am - SSN - [20200923-0428] - [002] - M04-06 - Create informational messages that timeout

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0719: Login - Start");

                _viewModel.Entity.Password = txtPassword.Password;

                _viewModel.Login();
            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("ps-253-20220415-0718: Login -Failed", ex);

                throw;
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0717: Login - Cancel");
            _viewModel.Close();
        }

    }
}
