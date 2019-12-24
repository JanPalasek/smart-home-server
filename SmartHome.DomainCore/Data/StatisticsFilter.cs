using System;
using SmartHome.Common;

namespace SmartHome.DomainCore.Data
{
    public class StatisticsFilter : ICloneable<StatisticsFilter>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        
        public GroupByType? GroupBy { get; set; }
        
        /// <summary>
        /// True, if only get inside measurement statistics. Is set by framework, not by user.
        /// </summary>
        public bool? IsInside { get; set; }

        public StatisticsFilter Clone()
        {
            return (StatisticsFilter)MemberwiseClone();
        }
    }
}