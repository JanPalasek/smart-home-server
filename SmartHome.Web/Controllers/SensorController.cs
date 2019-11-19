using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType;
using SmartHome.DomainCore.ServiceInterfaces.Place;
using SmartHome.DomainCore.ServiceInterfaces.Sensor;
using SmartHome.DomainCore.ServiceInterfaces.SensorType;
using SmartHome.Web.Models.Sensor;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class SensorController : Controller
    {
        private readonly IGetSensorTypesService getSensorTypesService;
        private readonly IGetSensorsService getSensorsService;
        private readonly ICreateSensorService createSensorService;
        private readonly IUpdateSensorService updateSensorService;
        private readonly IDeleteSensorService deleteSensorService;
        private readonly IGetPlacesService getPlacesService;
        private readonly IGetBatteryPowerSourceTypesService getBatteryPowerSourceTypesService;

        public SensorController(IGetSensorTypesService getSensorTypesService,
            IGetSensorsService getSensorsService, ICreateSensorService createSensorService,
            IUpdateSensorService updateSensorService, IDeleteSensorService deleteSensorService,
            IGetPlacesService getPlacesService, IGetBatteryPowerSourceTypesService getBatteryPowerSourceTypesService)
        {
            this.getSensorTypesService = getSensorTypesService;
            this.getSensorsService = getSensorsService;
            this.createSensorService = createSensorService;
            this.updateSensorService = updateSensorService;
            this.deleteSensorService = deleteSensorService;
            this.getPlacesService = getPlacesService;
            this.getBatteryPowerSourceTypesService = getBatteryPowerSourceTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            SensorModel sensorModel = await getSensorsService.GetSensorByIdAsync(id);

            var vm = await CreateAndFillViewModelAsync(sensorModel, false);
            
            return View("Detail", vm);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(SensorModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await updateSensorService.UpdateSensorAsync(model);
                if (result.ValidationResult.Succeeded)
                {
                    return RedirectToAction("Detail", new { Id = result.Value });
                }

                ModelState.AddValidationErrors(result.ValidationResult);
            }
            
            return View("Detail", new SensorViewModel(model) {IsCreatePage = false});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var vm = await CreateAndFillViewModelAsync(new SensorModel(), true);

            return View("Detail", vm);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SensorModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createSensorService.CreateSensorAsync(model);
                if (result.ValidationResult.Succeeded)
                {
                    return RedirectToAction("Detail", new { Id = result.Value });
                }

                ModelState.AddValidationErrors(result.ValidationResult);
            }
            
            return View("Detail", new SensorViewModel(model) {IsCreatePage = true});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            await deleteSensorService.DeleteSensorAsync(id);
            return RedirectToAction("List");
        }
        
        [HttpGet]

        public async Task<IActionResult> List()
        {
            var items = await getSensorsService.GetAllSensorsAsync();
            
            return View("List", new SensorListViewModel(items)
            {
                CanCreate = User.IsInRole("Admin")
            });
        }

        private async Task<SensorViewModel> CreateAndFillViewModelAsync(SensorModel model, bool isCreatePage)
        {
            var viewModel = new SensorViewModel(model)
            {
                IsCreatePage = isCreatePage,
                CanEdit = User.IsInRole("Admin")
            };

            viewModel.BatteryPowerSourceTypes = await getBatteryPowerSourceTypesService.GetAllPowerSourceTypesAsync();
            viewModel.SensorTypes = await getSensorTypesService.GetAllSensorTypesAsync();
            viewModel.Places = await getPlacesService.GetAllPlacesAsync();

            return viewModel;
        }
    }
}