﻿using System;
using System.Windows;
using WPF.Sample.AppLayer;

namespace WPF.Sample
{
    public partial class App : Application
    {
        // 04/14/2022 09:04 am - SSN
        public static bool HaveDatabaseConnection = false;
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
        }
    }
}
