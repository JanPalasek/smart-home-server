using System.Collections.Generic;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Unit
{
    public class UnitViewModel : SmartHomeViewModel<UnitModel>
    {
        public bool ReadOnly { get; set; }
        
        public IEnumerable<BatteryPowerSourceTypeModel> BatteryPowerSourceTypes { get; set; }
        public IEnumerable<UnitTypeModel> UnitTypes { get; set; }
    }
}