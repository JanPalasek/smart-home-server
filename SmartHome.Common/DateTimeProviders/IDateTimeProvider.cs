using System;

namespace SmartHome.Common.DateTimeProviders
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}