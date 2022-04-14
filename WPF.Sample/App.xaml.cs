using System;
using System.Collections.Generic;
using System.Windows;
using WPF.Sample.AppLayer;
using WPF.Sample.ViewModelLayer;

namespace WPF.Sample
{
    public partial class App : Application
    { 
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set the DataDirectory for Entity Framework
            string path = Environment.CurrentDirectory;
            path = path.Replace(@"\bin\Debug", "");
            path += @"\App_Data\";

            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            // Load Application Settings
            AppSettings.Instance.LoadSettings();

            APP_INSIGHTS.configure();

            
            Dictionary<string, string> dic = new Dictionary<string, string>();

            APP_INSIGHTS.ai.TrackEvent("App start",dic);

        }
        protected override void OnExit(ExitEventArgs e)
        {
            APP_INSIGHTS.ai.Dispose();

            base.OnExit(e);
        }

    }
}
