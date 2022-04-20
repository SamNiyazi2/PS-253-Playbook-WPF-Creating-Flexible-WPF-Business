

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 04/14/2022 10:32 am - SSN 
// https://docs.microsoft.com/en-us/azure/azure-monitor/app/windows-desktop

// 04/16/2022 12:04 am - SSN - Moved from WPF.Common
// namespace WPF.Common
// 04/18/2022 04:02 am  SSN - For resue
// namespace Common.Library
//
//
// install-package microsoft.ApplicationInsights -Version 2.20.0
//
//

namespace ssn_application_insights
{

    public static class APP_INSIGHTS
    {
        public static ApplicationInsightsUtil ai = new ApplicationInsightsUtil();

        public static void configure()
        {
            ApplicationInsightsUtil.setupApplicationInsights();
        }
    }


    public class ApplicationInsightsUtil : IDisposable
    {
        private TelemetryClient tc = new TelemetryClient();

        public ApplicationInsightsUtil()
        {
            // Alternative to setting ikey in config file:
            //////////////////////////////////////////////////////  tc.InstrumentationKey = "key copied from portal";

            // Set session data:
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            tc.Context.Cloud.RoleInstance = "ps-253-WPF-destop-AI";

        }
        public static void setupApplicationInsights()
        {
            TelemetryConfiguration.Active.InstrumentationKey = "77636339-36d0-4495-b720-37e34723d93f";
        }

        public void TrackPageView(string name, TimeSpan duration, IDictionary<string, string> props, IDictionary<string, double> metrics)
        {
            PageViewTelemetry pvt = new PageViewTelemetry();

            pvt.Name = name;
            pvt.Timestamp = DateTime.UtcNow;
            pvt.Duration = duration;

            foreach (KeyValuePair<string, string> e in props ?? emptyProps)
            {
                pvt.Properties.Add(e);
            }

            foreach (KeyValuePair<string, double> e in metrics ?? emptyMetrics)
            {
                pvt.Metrics.Add(e);
            }

            tc.TrackPageView(pvt);
        }


        // # // pragma warning disable CA0649

        Dictionary<string, string> emptyProps = new Dictionary<string, string>();
        Dictionary<string, double> emptyMetrics = new Dictionary<string, double>();

        public void TrackEvent(string name, IDictionary<string, string> props = null, IDictionary<string, double> metrics = null)
        {
            EventTelemetry et = new EventTelemetry();
            et.Name = name;
            et.Timestamp = DateTime.UtcNow;

            foreach (KeyValuePair<string, string> e in props ?? emptyProps)
            {
                et.Properties.Add(e);
            }

            foreach (KeyValuePair<string, double> e in metrics ?? emptyMetrics)
            {
                et.Metrics.Add(e);
            }

            tc.TrackEvent(et);
        }

        public void TrackException(string message, Exception ex, IDictionary<string, string> props = null)
        {
            ExceptionTelemetry et = new ExceptionTelemetry();

            et.Exception = ex;
            et.Message = message;
            et.Timestamp = DateTime.UtcNow;

            foreach (KeyValuePair<string, string> e in props ?? emptyProps)
            {
                et.Properties.Add(e);
            }
            et.Properties.Add("Test-101", "Value-101");

            Exception iex = ex;
            int exceptionCount = 0; 
            while ( iex != null)
            {
                exceptionCount++;
                et.Properties.Add($"Exception-{exceptionCount:000}", ex.Message);
                iex = ex.InnerException;
            }

            tc.TrackException(et);

        }

        public void Dispose()
        {
            if (tc != null)
            {
                tc.Flush(); // only for desktop apps

                // Allow time for flushing:
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
