using Common.Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 09/23/2020 08:23 am - SSN - [20200923-0823] - [001] - M07-03 - Demo: Entity Framework classes and validation 

namespace WPF.Sample.DataLayer
{
    [Table("UserFeedback")]
    public class UserFeedback : CommonBase
    {

        #region Properties

        private int _UserFeedbackId;

        [Required]
        [Key]
        public int UserFeedbackId
        {
            get { return _UserFeedbackId; }
            set
            {
                _UserFeedbackId = value;
                RaisePropertyChanged("UserFeedbackId");

            }
        }

        private string _Name;

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged("Name");

            }
        }


        private string _EmailAddress;

        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                _EmailAddress = value;
                RaisePropertyChanged("EmailAddress");

            }
        }


        private string _PhoneExtension;

        public string PhoneExtension
        {
            get { return _PhoneExtension; }
            set
            {
                _PhoneExtension = value;
                RaisePropertyChanged("PhoneExtension");

            }
        }


        private string _Message;

        [Required(ErrorMessage = "Input is required")]
        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                RaisePropertyChanged("Message");

            }
        }

        #endregion Properties

    }
}
