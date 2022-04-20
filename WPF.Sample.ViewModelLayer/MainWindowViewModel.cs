using Common.Library;

using System.Timers;
using WPF.Sample.DataLayer;

namespace WPF.Sample.ViewModelLayer
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Variables

        // 09/22/2020 04:17 pm - SSN - [20200922-1617] - [001] - M02--08 - Demo: load resource in the background
        private const int SECONDS = 1500;


        // 09/23/2020 04:30 am - SSN - [20200923-0428] - [001] - M04-06 - Create informational messages that timeout
        private Timer _InfoMessageTimer = null;
        private int _InfoMessageTimeout;

        private User _UserEntity = new User();

        private string _LoginMenuHeader = "Login";
        private string _StatusMessage;

        // 09/22/2020 02:48 pm - SSN - [20200922-1448] - [001] - M02-05 - Create properties for information messages 
        private bool _IsInfoMessageVisible = true;
        private string _InfoMessage = string.Empty;
        private string _InfoMessage_2 = string.Empty;
        private string _InfoMessageTitle = string.Empty;


        #endregion


        #region Public Properties


        public User UserEntity
        {
            get { return _UserEntity; }
            set
            {
                _UserEntity = value;
                RaisePropertyChanged("UserEntity");

            }
        }

        public int InfoMessageTimeout
        {
            get { return _InfoMessageTimeout; }
            set
            {
                _InfoMessageTimeout = value;
                RaisePropertyChanged("InfoMessageTimeout");

            }
        }

        public string LoginMenuHeader
        {
            get { return _LoginMenuHeader; }
            set
            {
                _LoginMenuHeader = value;
                RaisePropertyChanged("LoginMenuHeader");
            }
        }

        public string StatusMessage
        {
            get { return _StatusMessage; }
            set
            {
                _StatusMessage = value;
                RaisePropertyChanged("StatusMessage");
            }
        }



        public bool IsInfoMessageVisible
        {
            get { return _IsInfoMessageVisible; }
            set
            {
                _IsInfoMessageVisible = value;
                RaisePropertyChanged("IsInfoMessageVisible");
            }
        }



        public string InfoMessage
        {
            get { return _InfoMessage; }
            set
            {
                _InfoMessage = value;
                RaisePropertyChanged("InfoMessage");

            }
        }


        public string InfoMessage_2
        {
            get { return _InfoMessage_2; }
            set
            {
                _InfoMessage_2 = value;
                RaisePropertyChanged("InfoMessage_2");

            }
        }


        public string InfoMessageTitle
        {
            get { return _InfoMessageTitle; }
            set
            {
                _InfoMessageTitle = value;
                RaisePropertyChanged("InfoMessageTitle");

            }
        }

        // 04/14/2022 07:53 am - SSN 
        private bool _haveValidConnection;
         
        public bool HaveValidConnection
        {
            get { return _haveValidConnection; }
            set
            {
                _haveValidConnection = value;
                RaisePropertyChanged("HaveValidConnection");
            }
        }



        #endregion


        public void LoadStateCodes()
        {
            // Todo: Write code to load state codes here
            System.Threading.Thread.Sleep(SECONDS);
        }

        public void LoadCountryCodes()
        {
            // Todo: Write code to load country codes here
            System.Threading.Thread.Sleep(SECONDS);
        }

        public void LoadEmployeeTypes()
        {
            //Todo: 
            System.Threading.Thread.Sleep(SECONDS);
        }

        public void ClearInfoMessages()
        {
            InfoMessage = string.Empty;
            InfoMessage_2 = string.Empty;
            InfoMessageTitle = string.Empty;
            IsInfoMessageVisible = false;
        }




        public virtual void CreateInfoMessgaeTimer()
        {
            if (_InfoMessageTimer == null)
            {
                _InfoMessageTimer = new Timer(_InfoMessageTimeout);
                _InfoMessageTimer.Elapsed += _InfoMessageTimer_Elapsed;
            }

            _InfoMessageTimer.AutoReset = false;
            _InfoMessageTimer.Enabled = true;
            IsInfoMessageVisible = true;
        }

        private void _InfoMessageTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsInfoMessageVisible = false;
        }

    }
}
