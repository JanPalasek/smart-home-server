using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.SensorType;
using SmartHome.Web.Models.Sensor;
using SmartHome.Web.Models.SensorType;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class SensorTypeController : Controller
    {
        private readonly ICreateSensorTypeService createSensorTypeService;
        private readonly IUpdateSensorTypeService updateSensorTypeService;
        private readonly IGetSensorTypesService getSensorTypesService;
        private readonly IDeleteSensorTypeService deleteSensorTypeService;

        public SensorTypeController(ICreateSensorTypeService createSensorTypeService,
            IUpdateSensorTypeService updateSensorTypeService, IGetSensorTypesService getSensorTypesService,
            IDeleteSensorTypeService deleteSensorTypeService)
        {
            this.createSensorTypeService = createSensorTypeService;
            this.updateSensorTypeService = updateSensorTypeService;
            this.getSensorTypesService = getSensorTypesService;
            this.deleteSensorTypeService = deleteSensorTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            SensorTypeModel sensorModel = await getSensorTypesService.GetByIdAsync(id);

            var vm = new SensorTypeViewModel(sensorModel)
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = false
            };

            return View("Detail", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(SensorTypeModel model)
        {
            if (ModelState.IsValid)
            {
                await updateSensorTypeService.UpdateAsync(model);

                return RedirectToAction("Detail", new {id = model.Id});
            }

            return View("Detail", new SensorTypeViewModel(model)
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = false
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new SensorTypeViewModel(new SensorTypeModel())
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = true
            };

            return View("Detail", vm);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SensorTypeModel model)
        {
            if (ModelState.IsValid)
            {
                long id = await createSensorTypeService.CreateAsync(model);

                return RedirectToAction("Detail", new {id});
            }

            return View("Detail", new SensorTypeViewModel(model) {CanEdit = User.IsInRole("Admin"), IsCreatePage = true});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            await deleteSensorTypeService.DeleteAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sensorTypes = await getSensorTypesService.GetAllSensorTypesAsync();
            return View("List", new SensorTypeListViewModel(sensorTypes) { CanCreate = User.IsInRole("Admin") });
        }
    }
}