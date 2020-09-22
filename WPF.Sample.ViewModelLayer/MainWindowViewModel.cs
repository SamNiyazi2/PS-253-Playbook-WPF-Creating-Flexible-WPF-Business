using Common.Library;

namespace WPF.Sample.ViewModelLayer
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Variables
        private string _LoginMenuHeader = "Login";
        private string _StatusMessage;

        // 09/22/2020 02:48 pm - SSN - [20200922-1448] - [001] - M02-05 - Create properties for information messages 
        private bool _IsInfoMessageVisible = true;
        private string _InfoMessage = string.Empty;
        private string _InfoMessageTitle = string.Empty;


        #endregion


        #region Public Properties
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



        public string InfoMessageTitle
        {
            get { return _InfoMessageTitle; }
            set
            {
                _InfoMessageTitle = value;
                RaisePropertyChanged("InfoMessageTitle");

            }
        }





        #endregion
    }
}
