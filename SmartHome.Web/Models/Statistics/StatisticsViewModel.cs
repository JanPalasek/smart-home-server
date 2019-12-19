using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Statistics
{
    public class StatisticsViewModel
    {
        public IList<TemperatureMeasurementModel> Items { get; set; } = null!;
    }
}