using System;
using System.Diagnostics;

namespace SmartHome.Common.DateTimeProviders
{
    public class DynamicDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime dateTime;
        private readonly Stopwatch sw;

        public DynamicDateTimeProvider(DateTime dateTime)
        {
            this.dateTime = dateTime;
            sw = Stopwatch.StartNew();
        }

        public DateTime Now => dateTime + sw.Elapsed;
        public DateTime Today => (dateTime + sw.Elapsed).Date;
    }
}