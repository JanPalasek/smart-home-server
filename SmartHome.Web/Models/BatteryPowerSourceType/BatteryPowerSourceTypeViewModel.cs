using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.BatteryPowerSourceType
{
    public class BatteryPowerSourceTypeViewModel
    {
        public bool IsCreatePage { get; set; }
        public bool CanEdit { get; set; }
        public BatteryPowerSourceTypeModel Model { get; set; }

        public BatteryPowerSourceTypeViewModel(BatteryPowerSourceTypeModel model)
        {
            this.Model = model;
        }
    }
}