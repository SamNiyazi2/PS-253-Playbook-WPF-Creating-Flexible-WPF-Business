using ssn_AzureKeyVault.Interfaces;
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
        // No await
#pragma warning disable CS1998

        // 04/14/2022 07:29 am - SSN - Return task to catch error
        // public static void seedDatabase()
        public static void seedDatabaseAsync(SampleDbContext sampleDbContext)
        {

            if (!sampleDbContext.Users.Any())
            {
                User user = new User { FirstName = "John", LastName = "Doe", EmailAddress = "johnd@mail.com", UserName = "JohnD", Password = "Pa$$w)RD#" };
                sampleDbContext.Users.Add(user);
                sampleDbContext.SaveChanges();
            }

        }
    }
}
