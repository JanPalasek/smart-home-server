using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Sensor
{
    public class SensorViewModel : SmartHomeViewModel<SensorModel>
    {
        public bool ReadOnly { get; set; }
        
        public IEnumerable<SelectListItem> BatteryPowerSourceTypes { get; set; }
        public IEnumerable<SelectListItem> SensorTypes { get; set; }
        
        public IEnumerable<SelectListItem> Places { get; set; }
    }
}