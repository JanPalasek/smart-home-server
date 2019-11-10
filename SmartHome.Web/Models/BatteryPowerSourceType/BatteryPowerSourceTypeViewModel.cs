using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.BatteryPowerSourceType
{
    public class BatteryPowerSourceTypeViewModel
    {
        public bool ReadOnly { get; set; }
        public BatteryPowerSourceTypeModel Model { get; set; }

        public BatteryPowerSourceTypeViewModel(BatteryPowerSourceTypeModel model)
        {
            this.Model = model;
        }
    }
}