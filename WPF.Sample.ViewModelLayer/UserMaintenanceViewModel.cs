using Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 09/23/2020 04:19 am - SSN - [20200923-0404] - [003] - M04-04-Demo-Create-use-feedback-view-model (and UserMaintenance)

namespace WPF.Sample.ViewModelLayer
{
    public class UserMaintenanceViewModel : ViewModelBase
    {
        public UserMaintenanceViewModel()
        {
            DisplayStatusMessage("Maintain users");
        }
    }
}
