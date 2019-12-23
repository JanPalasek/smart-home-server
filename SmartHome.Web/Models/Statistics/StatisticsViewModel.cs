using System.Collections.Generic;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Statistics
{
    public class StatisticsViewModel
    {
        public StatisticsFilter Filter { get; }

        public StatisticsViewModel(StatisticsFilter filter)
        {
            Filter = filter;
        }
    }
}