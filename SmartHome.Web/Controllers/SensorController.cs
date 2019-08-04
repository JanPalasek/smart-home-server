using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Sensor;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorRepository repository;
        private readonly IBatteryPowerSourceTypeRepository batteryPowerSourceTypeRepository;
        private readonly ISensorTypeRepository sensorTypeRepository;
        private readonly IPlaceRepository placeRepository;

        public SensorController(
            ISensorRepository repository,
            IBatteryPowerSourceTypeRepository batteryPowerSourceTypeRepository,
            ISensorTypeRepository sensorTypeRepository,
            IPlaceRepository placeRepository)
        {
            this.repository = repository;
            this.batteryPowerSourceTypeRepository = batteryPowerSourceTypeRepository;
            this.sensorTypeRepository = sensorTypeRepository;
            this.placeRepository = placeRepository;
        }

        public async Task<IActionResult> Detail(int? id)
        {
            SensorModel sensorModel;
            if (id != null)
            {
                sensorModel = await repository.SingleAsync(id.Value);
            }
            else
            {
                sensorModel = new SensorModel();
            }

            var vm = await FillViewModelAsync();
            vm.Model = sensorModel;
            
            return View("Detail", vm);
        }
        
        public async Task<IActionResult> Detail(SensorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new SensorViewModel {Model = model});
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }

        private async Task<SensorViewModel> FillViewModelAsync(SensorViewModel viewModel = null)
        {
            if (viewModel == null)
            {
                viewModel = new SensorViewModel();
            }

            var batteryPowerSourceTypesTask = batteryPowerSourceTypeRepository.GetAllAsync();
            var sensorTypesTask = sensorTypeRepository.GetAllAsync();
            var placesTask = placeRepository.GetAllAsync();

            viewModel.BatteryPowerSourceTypes = (await batteryPowerSourceTypesTask).ToSelectListItems(x => x.Id, x => $"{x.BatteryType}, {x.MaximumVoltage}V");
            viewModel.SensorTypes = (await sensorTypesTask).ToSelectListItems(x => x.Id, x => x.Name);
            viewModel.Places = (await placesTask).ToSelectListItems(x => x.Id, x => $"{x.Name}, in: {x.IsInside}");

            return viewModel;
        }
    }
}