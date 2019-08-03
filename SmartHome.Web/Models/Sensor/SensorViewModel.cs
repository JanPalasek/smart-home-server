using System.Collections.Generic;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Sensor
{
    public class SensorViewModel : SmartHomeViewModel<SensorModel>
    {
        public bool ReadOnly { get; set; }
        
        public IEnumerable<BatteryPowerSourceTypeModel> BatteryPowerSourceTypes { get; set; }
        public IEnumerable<SensorTypeModel> SensorTypes { get; set; }
    }
}