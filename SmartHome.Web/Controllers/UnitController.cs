using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Unit;

namespace SmartHome.Web.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitRepository repository;
        private readonly IBatteryPowerSourceTypeRepository batteryPowerSourceTypeRepository;
        private readonly IUnitTypeRepository unitTypeRepository;

        public UnitController(
            IUnitRepository repository,
            IBatteryPowerSourceTypeRepository batteryPowerSourceTypeRepository,
            IUnitTypeRepository unitTypeRepository)
        {
            this.repository = repository;
            this.batteryPowerSourceTypeRepository = batteryPowerSourceTypeRepository;
            this.unitTypeRepository = unitTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            UnitModel unitModel;
            if (id != null)
            {
                unitModel = await repository.SingleAsync(id.Value);
            }
            else
            {
                unitModel = new UnitModel();
            }

            var vm = await FillViewModelAsync();
            vm.Model = unitModel;
            
            return View("Detail", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Detail(UnitModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new UnitViewModel {Model = model});
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }

        private async Task<UnitViewModel> FillViewModelAsync(UnitViewModel viewModel = null)
        {
            if (viewModel == null)
            {
                viewModel = new UnitViewModel();
            }

            var batteryPowerSourceTypesTask = batteryPowerSourceTypeRepository.GetAllAsync();
            var unitTypesTask = unitTypeRepository.GetAllAsync();

            viewModel.BatteryPowerSourceTypes = await batteryPowerSourceTypesTask;
            viewModel.UnitTypes = await unitTypesTask;

            return viewModel;
        }
    }
}