using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.BatteryPowerSourceType
{
    public class BatteryPowerSourceTypeViewModel : SmartHomeViewModel<BatteryPowerSourceTypeModel>
    {
        public bool ReadOnly { get; set; }

        public BatteryPowerSourceTypeViewModel(BatteryPowerSourceTypeModel model) : base(model)
        {
        }
    }
}