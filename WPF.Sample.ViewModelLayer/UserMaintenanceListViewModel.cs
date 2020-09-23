using Common.Library;
using System;
using System.Collections.ObjectModel;
using WPF.Sample.DataLayer;

// 09/23/2020 11:40 am - SSN - [20200923-1125] - [002] - M08 - A design pattern for creating master / detail screen

namespace WPF.Sample.ViewModelLayer
{
    public class UserMaintenanceListViewModel : ViewModelBase
    {

        private ObservableCollection<User> _Users = new ObservableCollection<User>();

        public ObservableCollection<User> Users
        {
            get { return _Users; }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");

            }
        }


        public virtual void LoadUsers()
        {
            SampleDbContext db = null;

            try
            {
                db = new SampleDbContext();

                Users = new ObservableCollection<User>(db.Users);
            }
            catch (Exception ex)
            {
                PublishException(ex);
            }

        }


    }
}
