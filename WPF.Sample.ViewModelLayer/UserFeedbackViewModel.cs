using Common.Library;
using ssn_application_insights;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using WPF.Sample.DataLayer;


// 09/23/2020 04:04 am - SSN - [20200923-0404] - [001] - M04-04-Demo-Create-use-feedback-view-model

namespace WPF.Sample.ViewModelLayer
{
    public class UserFeedbackViewModel : ViewModelBase
    {
        public UserFeedbackViewModel()
        {
            DisplayStatusMessage("Submit user feedback");
        }

        // 09/23/2020 10:40 am - SSN - [20200923-1040] - [001] - M07-04 - Demo: Add Save and SendFeedback methods to view model 
        private UserFeedback _Entity = new UserFeedback();

        public UserFeedback Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");

            }
        }


        public bool Save()
        {
            bool ret = false;

            SampleDbContext db = null;

            try
            {
                db = new SampleDbContext();

                db.UserFeedback.Add(Entity);
                db.SaveChanges();
                ret = true;
            }
            catch (DbEntityValidationException ex)
            {
                ValidationMessages = new ObservableCollection<ValidationMessage>(db.CreateValidationMessages(ex));
                IsValidationVisible = true;

                APP_INSIGHTS.ai.TrackException("ps-253-20220416-0021 - User feedback - Save failed Db validation", ex);
 
            }
            catch (Exception ex)
            {

                PublishException(ex);
            }

            return ret;
        }


        public bool SendFeedback()
        {
            bool ret = false;

            if (Save())
            {
                MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE, "Feedback message sent.");

                ret = true;

                Close(false);
            }

            return ret;

        }


    }
}
