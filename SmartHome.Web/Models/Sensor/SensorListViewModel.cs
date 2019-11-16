using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Sensor
{
    public class SensorListViewModel
    {
        public SensorListViewModel(IEnumerable<SensorModel> items)
        {
            Items = items;
        }

        public bool CanCreate { get; set; }
        public IEnumerable<SensorModel> Items { get; set; }
    }
}