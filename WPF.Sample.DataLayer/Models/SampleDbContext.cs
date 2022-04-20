using Common.Library;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using ssn_AzureKeyVault;
using System.Threading.Tasks;
using ssn_AzureKeyVault.Models;
using System;
using WPF.Common;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using ssn_AzureKeyVault.SqlAzure;
using ssn_application_insights;
using System.Runtime.CompilerServices;

namespace WPF.Sample.DataLayer
{
    public partial class SampleDbContext : DbContext
    {
        // 04/13/2022 07:20 am - SSN - Check connectionstring
        #region Override connectionString
        // public SampleDbContext() : base("name=Samples")

        string accessToken_db;

        public SampleDbContext() : base(getConnectionString(), true) { }

        private static SqlConnection getConnectionString()
        {
            string connectionString = "";
            //string accessToken = "";

            object connectionString_obj = ConfigurationManager.ConnectionStrings["Samples"];
            ConfigSettings configSettings = ssn_AzureKeyVault.Configurations.ConfigurationReader.get<ConfigSettings>();


            if (connectionString_obj != null)
            {
                connectionString = connectionString_obj.ToString();
            }


            if (!connectionString.ToLower().StartsWith("azure"))
            {
                SqlConnection conn2 = new SqlConnection(connectionString);
                


                return conn2;
            }


            if (connectionString.ToLower() == "azure_me")
            {
                Task<SqlConnection> t = Task<SqlConnection>.Run(async () =>
                {

                    RestAccess restAccess = new RestAccess();

                    // Failed after passing
                    //ssn_MicrosoftToken temp1 = await restAccess.getTokenAsync_v2();
                    //ssn_AzureVaultRecord temp2 = await restAccess.getSecretAsync_v3("ssn-key-test-20210224-001");
                    // See error posted in method.
                    //ssn_AzureVaultRecord _ssn_AzureVaultRecord = await restAccess.getSecretAsync_v3("Azure-SQL-Password-20220418-B");

                    string temp2b = await ssn_AzureKeyVault.VaultDataAccess.getSecret("Azure-SQL-Password-20220418-B");


                    //string sqlServerPassword = _ssn_AzureVaultRecord.value;
                    string sqlServerPassword = temp2b;

                    string sqlConnectionString_me = String.Format(configSettings.sqlConnectionString_me_template, configSettings.serverName, configSettings.databaseName_v2, configSettings.azureSqlServerUsername, sqlServerPassword);

                    SqlConnection conn2 = new SqlConnection(sqlConnectionString_me);

                    return conn2;
                });

                t.Wait();

                return t.Result;

            }


            // IF the process fails, the system will attempt to create database named azure. Blank out.

            connectionString = "";

            try
            {

                // Tested OK
                Task t = Task.Run(async () =>
                  {

                      RestAccess restAccess = new RestAccess();
                      ssn_MicrosoftToken temp1 = restAccess.getTokenAsync_v2().Result;

                      string temp2 = await ssn_AzureKeyVault.VaultDataAccess.getSecret("ssn-key-test-20210224-001");
                      string temp2b = await ssn_AzureKeyVault.VaultDataAccess.getSecret("Azure-SQL-Password-20220418-B");

                      object connectionStringObj = ConfigurationManager.ConnectionStrings["Samples"];

                      if (connectionStringObj != null) connectionString = connectionStringObj.ToString();

                      if (string.IsNullOrEmpty(connectionString)) { connectionString = "Samples"; }


                  });

                t.Wait();


                Task<SqlConnection> t1 = Task<SqlConnection>.Factory.StartNew(() =>
             {
                 RestAccess restAccess = new RestAccess();

                 ssn_MicrosoftToken temp = restAccess.getTokenAsync_v2().Result;
                 // Todo check access_token in restAccess
                 if (!string.IsNullOrWhiteSpace(temp.error))
                 {
                     APP_INSIGHTS.ai.TrackEvent("ps-253-20220414-0714-B: Failed to get access token.");
                 }

                 ssn_AzureVaultRecord temp2 = restAccess.getSecretAsync_v3("ssn-key-test-20210224-001").Result;
                 ssn_AzureVaultRecord temp2b = restAccess.getSecretAsync_v3("Azure-SQL-Password-20220418-B").Result;


                 /////////////////////////////////////////////////   connectionString = temp2.value;


                 // 04/18/2022 03:14 am - SSN - [20220418-0249] - [002] - Sql Azure Access Token
                 // (Project 253 + 315 )
                 // Failing on app.AcquireTokenInteractive(scopes).ExecuteAsync();  No error message. Referenced project in directly.  No NuGet.

                 // Fails
                 /////////////////// string SqlAzureAccessToekn = await SqlAzureAccessToken.GetAccessToken_UserInteractive();

                 // Fails
                 /////////////////// string SqlAzureAccessToekn2 = await SqlAzureAccessToken.GetAccessToken_ClientCredentials();

                 // VaultDataAccess.connectToAzureSQL();
                 ConnectToAzureSQL connectToAzureSQL = new ConnectToAzureSQL();

                 Task t2 = connectToAzureSQL.connectToAzureSQLAsync();
                 t2.Wait();


                 connectionString = string.Format(configSettings.sqlConnectionString_template_forToken, configSettings.serverName, configSettings.databaseName_v2);
                 string accessToken_db = connectToAzureSQL.access_token;

                 SqlConnection sqlConn = new SqlConnection(connectionString);
                 sqlConn.AccessToken = accessToken_db;

                 return sqlConn;

             });


                //  .ContinueWith(a =>
                //  {
                //     string errorMessage = "ps-253-20220415-0626-A: Failed to get access token.";
                //     APP_INSIGHTS.ai.TrackException(errorMessage, a.Exception);

                //     throw new Exception("ps-253-20220415-0626-B: Failed to get access token.");

                // }, TaskContinuationOptions.OnlyOnFaulted)
                //

                ;

                t1.Wait();
                return t1.Result;

            }
            catch (Exception ex)
            {
                string message = ex.Message;

                APP_INSIGHTS.ai.TrackException("ps-253-20220415-0651: DbContext - Exception ", ex);

                throw;
            }



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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


    }

}