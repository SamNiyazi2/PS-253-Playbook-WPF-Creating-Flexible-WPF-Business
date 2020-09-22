using System;
using System.Threading;
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
            await Dispatcher.BeginInvoke(new Action(() =>
              {
                  Thread.Sleep(2000);
                  try
                  {
                      _viewModel.IsInfoMessageVisible = false;
                  }
                  catch (Exception ex)
                  {

                      string error = ex.Message;
                  }

              }), DispatcherPriority.Background);
        }
    }
}
