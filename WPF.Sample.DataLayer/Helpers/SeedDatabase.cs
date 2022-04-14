using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 03/30/2022 06:31 pm - SSN

namespace WPF.Sample.DataLayer.Helpers
{
    public static class DatabaseHelpers
    {

        // 04/14/2022 07:29 am - SSN - Return task to catch error
        // public static void seedDatabase()
        public static async Task<Task> seedDatabase()
        { 

            return Task.Factory.StartNew(() =>
                 {
                    // throw new Exception("TEsting-20220414-0912");

                     SampleDbContext db = new SampleDbContext();
                     if (!db.Users.Any())
                     {
                         User user = new User { FirstName = "John", LastName = "Doe", EmailAddress = "johnd@mail.com", UserName = "JohnD", Password = "Pa$$w)RD#" };
                         db.Users.Add(user);
                         db.SaveChanges();
                     }

                 });
             
        }
    }
}
