using Common.Library;

namespace WPF.Sample.ViewModelLayer
{
  public class MainWindowViewModel : ViewModelBase
  {
    #region Private Variables
    private string _LoginMenuHeader = "Login";
    private string _StatusMessage;
    #endregion

    #region Public Properties
    public string LoginMenuHeader
    {
      get { return _LoginMenuHeader; }
      set {
        _LoginMenuHeader = value;
        RaisePropertyChanged("LoginMenuHeader");
      }
    }

    public string StatusMessage
    {
      get { return _StatusMessage; }
      set {
        _StatusMessage = value;
        RaisePropertyChanged("StatusMessage");
      }
    }
    #endregion
  }
}
