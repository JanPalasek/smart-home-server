using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.ServiceLoaders;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.BatteryPowerSourceType;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [ServiceFilter(typeof(TransactionFilter))]
    public class BatteryPowerSourceTypeController : Controller
    {
        private readonly IBatteryPowerSourceTypeRepository repository;

        public BatteryPowerSourceTypeController(IBatteryPowerSourceTypeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            BatteryPowerSourceTypeModel powerSourceType;
            if (id != null)
            {
                powerSourceType = await repository.SingleAsync(id.Value);
            }
            else
            {
                powerSourceType = new BatteryPowerSourceTypeModel();
            }
            
            return View("Detail", new BatteryPowerSourceTypeViewModel()
            {
                Model = powerSourceType
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Detail(BatteryPowerSourceTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new BatteryPowerSourceTypeViewModel()
                {
                    Model = model
                });
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }
    }
}