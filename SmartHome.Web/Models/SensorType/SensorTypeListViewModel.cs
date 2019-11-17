using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.SensorType
{
    public class SensorTypeListViewModel
    {
        public SensorTypeListViewModel(IEnumerable<SensorTypeModel> items)
        {
            Items = items;
        }

        public bool CanCreate { get; set; }
        public IEnumerable<SensorTypeModel> Items { get; set; }
    }
}