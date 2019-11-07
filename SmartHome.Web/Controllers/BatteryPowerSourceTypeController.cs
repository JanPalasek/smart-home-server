using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.Web.Models.BatteryPowerSourceType;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class BatteryPowerSourceTypeController : Controller
    {
        private readonly IBatteryPowerSourceTypeRepository repository;

        public BatteryPowerSourceTypeController(IBatteryPowerSourceTypeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            BatteryPowerSourceTypeModel powerSourceType = await repository.SingleAsync(id);
            
            return View("Detail", new BatteryPowerSourceTypeViewModel(powerSourceType));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new BatteryPowerSourceTypeViewModel(new BatteryPowerSourceTypeModel());

            return View("Detail", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(BatteryPowerSourceTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new BatteryPowerSourceTypeViewModel(model));
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }
    }
}