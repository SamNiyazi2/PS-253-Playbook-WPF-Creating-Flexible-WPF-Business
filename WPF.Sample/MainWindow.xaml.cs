using Common.Library;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WPF.Sample.UserControls;
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

            MessageBroker.Instance.MessageReceived += Instance_MessageReceived;
        }

        private void Instance_MessageReceived(object sender, MessageBrokerEventArgs e)
        {
            switch ( e.MessageName)
            {
                case MessageBrokerMessages.CLOSE_USER_CONTROL:
                    CloseUserControl();
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            string cmd = string.Empty;

            if (mnu.Tag != null)
            {

                cmd = mnu.Tag.ToString();

                if (cmd.Contains("."))
                {
                    LoadUserControl(cmd);
                }
                else
                {
                    ProcessMenuCommands(cmd);
                }
            }

        }


        // 09/23/2020 01:46 am - SSN - [20200923-0146] - [001] - M03-04 - Demo: write code to load and close user controls 
        private void CloseUserControl()
        {
            contentArea.Children.Clear();
        }

        public void DisplayUserControl(UserControl uc)
        {
            CloseUserControl();
            contentArea.Children.Add(uc);
        }

        private void LoadUserControl(string controlName)
        {
            Type ucType = null;
            UserControl uc = null;

            if (ShouldLoadUserControl(controlName))
            {

                ucType = Type.GetType(controlName);

                if (ucType == null)
                {
                    MessageBox.Show("The control: " + controlName + " does not exist.");
                }
                else
                {

                    uc = (UserControl)Activator.CreateInstance(ucType);
                    if (uc != null)
                    {
                        DisplayUserControl(uc);
                    }
                }
            }
        }

        private void ProcessMenuCommands(string command)
        {
            switch (command.ToLower())
            {
                case "exit":
                    this.Close();
                    break;

                default:
                    break;
            }
        }


        private bool ShouldLoadUserControl(string controlName)
        {
            bool ret = true;

            if (contentArea.Children.Count > 0)
            {
                if (((UserControl)contentArea.Children[0]).GetType().Name ==
                    controlName.Substring(controlName.LastIndexOf(".") + 1))
                {
                    ret = false;
                }
            }

            return ret;
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
