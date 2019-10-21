using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Sensor;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            SensorModel sensorModel = await repository.SingleAsync(id);

            var vm = await FillViewModelAsync();
            vm.Model = sensorModel;
            
            return View("Detail", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = await FillViewModelAsync();
            vm.Model = new SensorModel();

            return View("Detail", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(SensorModel model)
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

            viewModel.BatteryPowerSourceTypes = (await batteryPowerSourceTypeRepository.GetAllAsync()).ToSelectListItems(x => x.Id, x => $"{x.BatteryType}, {x.MaximumVoltage}V");
            viewModel.SensorTypes = (await sensorTypeRepository.GetAllAsync()).ToSelectListItems(x => x.Id, x => x.Name);
            viewModel.Places = (await placeRepository.GetAllAsync()).ToSelectListItems(x => x.Id, x => $"{x.Name}, in: {x.IsInside}");

            return viewModel;
        }
    }
}