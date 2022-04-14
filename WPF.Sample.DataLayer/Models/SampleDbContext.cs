using Common.Library;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using ssn_AzureKeyVault;
using System.Threading.Tasks;
using ssn_AzureKeyVault.Models;
using System;

namespace WPF.Sample.DataLayer
{
    public partial class SampleDbContext : DbContext
    {
        // 04/13/2022 07:20 am - SSN - Check connectionstring
        #region Override connectionString
        // public SampleDbContext() : base("name=Samples")
        public SampleDbContext() : base(getConnectionString()) { }

        static private string getConnectionString()
        {
            string connectionString = "Samples";

            try
            {

                //// Tested OK
                //    Task t = Task.Run(async () =>
                //      {
                //          string temp = await ssn_AzureKeyVault.VaultDataAccess.getSecret("ssn-key-test-20210224-001");

                //          object connectionStringObj = ConfigurationManager.ConnectionStrings["Samples"];

                //          if (connectionStringObj != null) connectionString = connectionStringObj.ToString();

                //          if (string.IsNullOrEmpty(connectionString)) { connectionString = "Samples"; }


                //      });

                //    t.Wait();



                ssn_MicrosoftToken temp = ssn_AzureKeyVault.RestAccess.getSecret("ssn-key-test-20210224-001");
                if (!string.IsNullOrWhiteSpace(temp.error))
                {
                    throw new Exception("ps-253-20220414-0714: Failed to get connection string.");

                }
            }
            catch (Exception ex )
            {
                string message = ex.Message;
                throw;
            }



            return connectionString;

        }

        #endregion Override connectionString

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFeedback> UserFeedback { get; set; }


        // 03/30/2022 06:09 pm - SSN
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ps-253");
            //            IList<User>  user = this.Database.SqlQuery<User>("select top 1 * from ps-253.users").ToList();

            setup_User(modelBuilder);
            setup_UserFeedback(modelBuilder);
        }


        // 03/31/2022 12:24 am - SSN 
        #region Fluent definitions
        private void setup_User(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .Property(x => x.EmailAddress)
              .IsRequired()
              .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(40);
        }

        private void setup_UserFeedback(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserFeedback>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<UserFeedback>()
                .Property(x => x.EmailAddress)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<UserFeedback>()
                .Property(x => x.PhoneExtension)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<UserFeedback>()
                .Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(2000);

        }

        #endregion Fluent definitions


        // 09/23/2020 10:14 am - SSN - [20200923-0823] - [002] - M07-03 - Demo: Entity Framework classes and validation 
        public List<ValidationMessage> CreateValidationMessages(DbEntityValidationException ex)
        {
            List<ValidationMessage> ret = new List<ValidationMessage>();

            foreach (DbValidationError error in ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors))
            {
                ret.Add(new ValidationMessage
                {
                    Message = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });

            }

            return ret;
        }


    }

}