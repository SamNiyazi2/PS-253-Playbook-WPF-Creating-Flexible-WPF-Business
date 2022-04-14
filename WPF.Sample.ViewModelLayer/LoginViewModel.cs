

// 09/23/2020 02:21 am - SSN - [20200923-0216] - [001] - M03-06 - Create the login view model class 

using Common.Library;
using System;
using System.Linq;
using WPF.Common;
using WPF.Sample.DataLayer;

namespace WPF.Sample.ViewModelLayer
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            DisplayStatusMessage("Login to application");

            // 03/30/2022 05:39 pm - SSN
            // Entity = new User {UserName = Environment.UserName };

            SampleDbContext db = new SampleDbContext();
            Entity = db.Users.OrderBy(r=>r.UserId).FirstOrDefault();

        }


        private User _Entity;

        public User Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");

            }
        }


        public bool validate()
        {

            Entity.IsLoggedIn = false;
            ValidationMessages.Clear();

            if (string.IsNullOrWhiteSpace(Entity.UserName))
            {
                AddValidationMessage("UserName", "User name is required");
            }

            if (string.IsNullOrWhiteSpace(Entity.Password))
            {
                AddValidationMessage("Password", "Password is required");
            }


            return ValidationMessages.Count == 0;
        }


        public bool ValidateCredentials()
        {

            bool isValid = false;
            SampleDbContext db = null;

            try
            {
                db = new SampleDbContext();

                // 03/30/2022 08:27 am - SSN
                // isValid = db.Users.Where(u => u.UserName == Entity.UserName && u.Password== Entity.Password).Count() > 0;

                User tempEntity = db.Users.Where(u => u.UserName == Entity.UserName && u.Password == Entity.Password).FirstOrDefault();

                //if (!isValid)
                if (tempEntity == null)
                {
                    AddValidationMessage("LoginFailed", "Invalid user name or password");
                }
                else
                {
                    isValid = true;
                    Entity = tempEntity;
                    Entity.Password = "";
                }
            }
            catch (Exception ex)
            {
                APP_INSIGHTS.ai.TrackException("Login - Failed", ex);

                PublishException(ex);
            }

            return isValid;

        }


        public bool Login()
        {
            bool ret = false;

            if (validate())
            {

                if (ValidateCredentials())
                {
                    Entity.IsLoggedIn = true;

                    MessageBroker.Instance.SendMessage(MessageBrokerMessages.LOGIN_SUCCESS, Entity);

                    Close(false);
                    ret = true;
                }
            }

            return ret;
        }


        public override void Close(bool wasCancelled = true)
        {
            if (wasCancelled)
            {
                MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE, "User not logged in.");
            }
            base.Close(wasCancelled);
        }

    }
}
