using Common.Library;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WPF.Sample.DataLayer;
using WPF.Sample.UserControls;
using WPF.Sample.ViewModelLayer;
using WPF.Common;
using System.Runtime.CompilerServices;

namespace WPF.Sample
{



    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = null;

        private string _originalMessage = string.Empty;
        private readonly SampleDbContext sampleDbContext;

        // public MainWindow()
        public MainWindow(SampleDbContext _sampleDbContext)
        {
            sampleDbContext = _sampleDbContext;


            InitializeComponent();
            _viewModel = (MainWindowViewModel)this.Resources["viewModel"];

            _originalMessage = _viewModel.StatusMessage;

            MessageBroker.Instance.MessageReceived += Instance_MessageReceived;

        }
        // 04/15/2022 03:01 am - SSN - Inject SampleDbContext

        //public MainWindow(SampleDbContext _sampleDbContext):this()
        //{
        //    sampleDbContext = _sampleDbContext;

        //}


        private void Instance_MessageReceived(object sender, MessageBrokerEventArgs e)
        {
            switch (e.MessageName)
            {

                case MessageBrokerMessages.LOGIN_SUCCESS:
                    _viewModel.UserEntity = (User)e.MessagePayload;

                    // 04/14/2022 09:10 am - SSN

                    _viewModel.UserEntity.HaveValidConnection = App.HaveDatabaseConnection;

                    _viewModel.LoginMenuHeader = "Logout " + _viewModel.UserEntity.FirstName;
                    break;

                case MessageBrokerMessages.LOGOUT:
                    _viewModel.UserEntity.IsLoggedIn = false;
                    _viewModel.LoginMenuHeader = "Login";
                    break;

                case MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE:
                    _viewModel.InfoMessageTitle = e.MessagePayload.ToString();
                    _viewModel.CreateInfoMessgaeTimer();
                    break;

                case MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE:
                    _viewModel.StatusMessage = e.MessagePayload.ToString();
                    _viewModel.CreateInfoMessgaeTimer();
                    break;

                case MessageBrokerMessages.DISPLAY_STATUS_MESSAGE:
                    _viewModel.StatusMessage = e.MessagePayload.ToString();
                    break;

                case MessageBrokerMessages.CLOSE_USER_CONTROL:
                    CloseUserControl();
                    break;
            }
        }

        // 03/30/2022 08:12 am - SSN 
        public User LoggedInUser
        {
            get
            {
                return _viewModel.UserEntity;
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
                    APP_INSIGHTS.ai.TrackEvent($"ps-253-20220415-0721: {cmd}");
                    LoadUserControl(cmd);
                }
                else
                {
                    APP_INSIGHTS.ai.TrackEvent($"ps-253-20220415-0720: {cmd}");
                    ProcessMenuCommands(cmd);
                }
            }

        }


        // 09/23/2020 01:46 am - SSN - [20200923-0146] - [001] - M03-04 - Demo: write code to load and close user controls 
        private void CloseUserControl()
        {
            contentArea.Children.Clear();
            _viewModel.StatusMessage = _originalMessage;
        }

        public void DisplayUserControl(UserControl uc)
        {

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
                    CloseUserControl();

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

                case "login":
                    if (_viewModel.UserEntity.IsLoggedIn)
                    {
                        CloseUserControl();
                        _viewModel.UserEntity = new User();
                        _viewModel.LoginMenuHeader = "Login";
                    }
                    else
                    {
                        LoadUserControl("WPF.Sample.UserControls.LoginControl");
                    }
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

        }


        // 09/22/2020 04:22 pm - SSN - [20200922-1617] - [002] - M02--08 - Demo: load resource in the background
        public async Task LoadApplication()
        {
            _viewModel.InfoMessage = "Connecting to database...";
            try
            {

                bool haveConnection = false;

                // 03/31/2022 12:48 am - SSN
                // 04/14/2022 09:06 am - SSN - Revise to pickup connection string from Azure vault.
                Task t1 = await DataLayer.Helpers.DatabaseHelpers.seedDatabaseAsync(sampleDbContext);

                Task t2 = t1.ContinueWith(async t =>
                    {
                        await Dispatcher.BeginInvoke(new Action(() =>
                        {

                            _viewModel.InfoMessageTitle = "Failed to connect to database.  (202)";
                            _viewModel.InfoMessage = "Click to close application";


                            this.MouseDown += MainWindow_MouseDown_FailedConnection;


                        }), DispatcherPriority.Background);

                        return t;

                    }, TaskContinuationOptions.OnlyOnFaulted);

                Task t3 = t1.ContinueWith(t =>
                     {
                         TaskStatus status = t.Status;

                         string test = "Ran to completion.";
                         haveConnection = true;
                         return t;

                     }, TaskContinuationOptions.OnlyOnRanToCompletion);

                 
                t1.Wait();
                Task.WaitAny(t2, t3);




                if (!haveConnection) return;

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

                _viewModel.UserEntity.HaveValidConnection = true;

                App.HaveDatabaseConnection = true;

                _viewModel.ClearInfoMessages();

            }
            catch (Exception ex)
            {
                _viewModel.InfoMessageTitle = "Failed to connect to database.  (101)";
                _viewModel.InfoMessage = "Click to close application";

                APP_INSIGHTS.ai.TrackException("ps-253-20220416-0024 - Main Window - Load application failure", ex);




                MessageBox.Show(@"System failed to start.

Error was logged and should be handled shortly.

(903)
", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // 04/16/2022 01:24 am - SSN - When connection string is invalid, the main window fails to load.  We managed to reach this point by setting task
                // "DispatcherPriority.Send" but the app is null once we reach this point.
                if (App.Current != null)
                {
                    App.Current.Shutdown(-903);
                }


            }


        }

        /// 04/14/2022 08:06 am - SSN
        private void MainWindow_MouseDown_FailedConnection(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
