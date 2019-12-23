using System;
using SmartHome.Common;

namespace SmartHome.DomainCore.Data
{
    public class StatisticsFilter : ICloneable<StatisticsFilter>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        
        public GroupByType? GroupBy { get; set; }

        public StatisticsFilter Clone()
        {
            return (StatisticsFilter)MemberwiseClone();
        }
    }
}