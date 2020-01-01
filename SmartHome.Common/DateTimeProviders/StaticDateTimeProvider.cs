using System;

namespace SmartHome.Common.DateTimeProviders
{
    public class StaticDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime staticDateTime;

        public StaticDateTimeProvider(DateTime staticDateTime)
        {
            this.staticDateTime = staticDateTime;
        }

        public DateTime Now => staticDateTime;
        public DateTime Today => staticDateTime.Date;
    }
}