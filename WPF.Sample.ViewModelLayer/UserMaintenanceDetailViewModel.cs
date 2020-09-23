using Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Sample.DataLayer;

// 09/23/2020 12:47 pm - SSN - [20200923-1247] - [001] - M08 - A design pattern for creating master / detail screen

namespace WPF.Sample.ViewModelLayer
{
    public class UserMaintenanceDetailViewModel : UserMaintenanceListViewModel
    {

        private User _Entity = new User();

        public User Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");

            }
        }


        public override void LoadUsers()
        {
            base.LoadUsers();

            if (Users.Count > 0)
            {
                Entity = Users[0];
            }

        }


    }


}
