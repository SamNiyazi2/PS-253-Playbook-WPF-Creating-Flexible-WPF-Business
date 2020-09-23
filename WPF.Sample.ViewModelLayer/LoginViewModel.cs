

// 09/23/2020 02:21 am - SSN - [20200923-0216] - [001] - M03-06 - Create the login view model class 

using Common.Library;

namespace WPF.Sample.ViewModelLayer
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            DisplayStatusMessage("Login to application");
        }

        public override void Close(bool wasCancelled = true)
        {
            if ( wasCancelled)
            {
                MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE, "User not logged in.");
            }
            base.Close(wasCancelled);
        }

    }
}
