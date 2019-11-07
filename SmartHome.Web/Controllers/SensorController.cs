using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
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

            var vm = await CreateAndFillViewModelAsync(sensorModel);
            
            return View("Detail", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = await CreateAndFillViewModelAsync(new SensorModel());

            return View("Detail", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(SensorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new SensorViewModel(model));
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }

        private async Task<SensorViewModel> CreateAndFillViewModelAsync(SensorModel model)
        {
            var viewModel = new SensorViewModel(model);

            viewModel.BatteryPowerSourceTypes = (await batteryPowerSourceTypeRepository.GetAllAsync())
                .ToSelectListItems(x => x.Id.ToString(), x => $"{x.BatteryType}, {x.MaximumVoltage}V");
            viewModel.SensorTypes = (await sensorTypeRepository.GetAllAsync())
                .ToSelectListItems(x => x.Id.ToString(), x => $"{x.Name}");
            viewModel.Places = (await placeRepository.GetAllAsync())
                .ToSelectListItems(x => x.Id.ToString(), x => $"{x.Name}, in: {x.IsInside}");

            return viewModel;
        }
    }
}