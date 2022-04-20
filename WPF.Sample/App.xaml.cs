using System;
using System.Collections.Generic;
using System.Windows;
using WPF.Sample.AppLayer;
using Common.Library;
using Microsoft.Extensions.DependencyInjection; 
using WPF.Sample.Extensions;
using System.Data.Entity;
using ssn_application_insights;

namespace WPF.Sample
{
    public partial class App : Application
    {
        // 04/14/2022 09:04 am - SSN
        public static bool HaveDatabaseConnection = false;
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the DataDirectory for Entity Framework
            string path = Environment.CurrentDirectory;
            path = path.Replace(@"\bin\Debug", "");
            path += @"\App_Data\";

            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            // Load Application Settings
            AppSettings.Instance.LoadSettings();





            try
            {
                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"System failed to start.

Error was logged and should be handled shortly.

(901)
", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);

                APP_INSIGHTS.ai.TrackException("fs-253-20220415-0637: MainWindow crash", ex);

                App.Current.Shutdown(-901);
            }

        }
        protected void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {

                addDependencyInjectionItems();

                APP_INSIGHTS.configure();


                Dictionary<string, string> dic = new Dictionary<string, string>();
                APP_INSIGHTS.ai.TrackEvent("ps-253-20220415-0724: App start", dic);

                var mainWindow = serviceProvider.GetService<MainWindow>();
                mainWindow.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(@"System failed to start.

Error was logged and should be handled shortly.

(902)
", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);

                APP_INSIGHTS.ai.TrackException("fs-253-20220415-0636: MainWindow crash", ex);

                App.Current.Shutdown(-902);
            }


        }



        // 04/14/2022 10:23 pm - SSN
        private ServiceProvider serviceProvider;

        private void addDependencyInjectionItems()
        {
            ServiceCollection services = new ServiceCollection();
              
            DataLayer.Helpers.External_DI_Helper.AddServices(services);

            EF_Ext.AddDbContext<DbContext>(services);
            services.AddTransient<MainWindow>();

            serviceProvider = services.BuildServiceProvider();


        }


        protected override void OnExit(ExitEventArgs e)
        {
            APP_INSIGHTS.ai.Dispose();

            base.OnExit(e);
        }

    }
}
