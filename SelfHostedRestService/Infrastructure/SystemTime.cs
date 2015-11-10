using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Infrastructure
{
    public class SystemTime
    {
        private static DateTime? date = null;

        public static void Set(DateTime custom)
        {
            date = custom;
        }

        public static void Reset()
        {
            date = null;
        }

        public static DateTime Now
        {
            get
            {
                if (date == null) return DateTime.Now;
                
                return (DateTime)date;
            }
        }
    }
}
