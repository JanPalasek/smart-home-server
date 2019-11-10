using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Sensor
{
    public class SensorViewModel
    {
        public SensorViewModel(
            SensorModel model)
        {
            Model = model;
        }

        public bool ReadOnly { get; set; }
        
        public SensorModel Model { get; set; }

        public IEnumerable<SelectListItem> BatteryPowerSourceTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> SensorTypes { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<SelectListItem> Places { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}