using ssn_application_insights;
using System;

namespace Common.Library
{
    public class ExceptionManager : CommonBase
    {
        #region Instance Property
        private static ExceptionManager _Instance;

        public static ExceptionManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ExceptionManager();
                }

                return _Instance;
            }
            set { _Instance = value; }
        }
        #endregion

        #region Publish Methods
        public virtual void Publish(Exception ex)
        {
            // TODO: Implement an exception publisher here
            System.Diagnostics.Debug.WriteLine(ex.ToString());

            // 04/15/2022 11:53 pm - SSN - Update
            APP_INSIGHTS.ai.TrackException("ps-253-20220416-0005: Publish error", ex);
        }
        #endregion
    }
}
