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
            _viewModel.Close();
        }


        private void SendFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SendFeedback();
        }


        private void ValidationListBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        { 
            inputScroller.ScrollToEnd();
        }
    }
}
