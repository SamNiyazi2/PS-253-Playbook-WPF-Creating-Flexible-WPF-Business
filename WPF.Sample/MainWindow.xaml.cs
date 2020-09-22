using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPF.Sample.ViewModelLayer;

namespace WPF.Sample
{



    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = null;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)this.Resources["viewModel"];
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            await LoadApplication();

            _viewModel.ClearInfoMessages();
        }


        // 09/22/2020 04:22 pm - SSN - [20200922-1617] - [002] - M02--08 - Demo: load resource in the background
        public async Task LoadApplication()
        {
            _viewModel.InfoMessage = "Loading state codes...";
            await Dispatcher.BeginInvoke(new Action(() =>
          {
              _viewModel.LoadStateCodes();
          }), DispatcherPriority.Background);


            _viewModel.InfoMessage = "Loading country codes...";
            await Dispatcher.BeginInvoke(new Action(() =>
         {
             _viewModel.LoadCountryCodes();

         }), DispatcherPriority.Background);


            _viewModel.InfoMessage = "Loading employee types...";
            await Dispatcher.BeginInvoke(new Action(() =>
          {
              _viewModel.LoadEmployeeTypes();

          }), DispatcherPriority.Background);






        }



    }
}
