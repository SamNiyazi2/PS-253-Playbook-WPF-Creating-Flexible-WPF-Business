using System;
using System.Windows;
using System.Windows.Controls;
using Common.Library;
using ssn_application_insights;
using WPF.Sample.ViewModelLayer;

// 09/23/2020 04:11 am - SSN - [20200923-0404] - [002] - M04-04-Demo-Create-use-feedback-view-model

namespace WPF.Sample.UserControls
{
    /// <summary>
    /// Interaction logic for UserFeedbackControl.xaml
    /// </summary>
    public partial class UserFeedbackControl : UserControl
    {
        private UserFeedbackViewModel _viewModel = null;

        public UserFeedbackControl()
        {
            InitializeComponent();

            _viewModel = (UserFeedbackViewModel)this.Resources["viewModel"];
            validationListBox.IsVisibleChanged += ValidationListBox_IsVisibleChanged;

        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0716: User feedback - Close");

            _viewModel.Close();
        }


        private void SendFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0715: Send feedback");
                _viewModel.SendFeedback();

            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("ps-253-20220415-0714: Send feedback - failed", ex);

                throw;
            }
        }


        private void ValidationListBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            inputScroller.ScrollToEnd();
        }
    }
}
