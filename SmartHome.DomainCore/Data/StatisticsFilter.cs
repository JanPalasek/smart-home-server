using System;

namespace SmartHome.DomainCore.Data
{
    public class StatisticsFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        
        public GroupByType? GroupBy { get; set; }
    }
}