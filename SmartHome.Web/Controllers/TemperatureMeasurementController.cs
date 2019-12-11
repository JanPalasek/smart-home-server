using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Place;
using SmartHome.DomainCore.ServiceInterfaces.Sensor;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;
using SmartHome.Web.Models.Sensor;
using SmartHome.Web.Models.TemperatureMeasurement;
using SmartHome.Web.Utils;
using Syncfusion.EJ2.Base;

namespace SmartHome.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class TemperatureMeasurementController : Controller
    {
        private readonly IGetTemperatureMeasurementsService getTemperatureMeasurementsService;
        private readonly IGetSensorsService getSensorsService;
        private readonly IGetPlacesService getPlacesService;
        private readonly IDeleteTemperatureMeasurementService deleteTemperatureMeasurementService;
        private readonly ICreateTemperatureMeasurementService createTemperatureMeasurementService;
        private readonly IUpdateTemperatureMeasurementService updateTemperatureMeasurementService;

        public TemperatureMeasurementController(
            IGetTemperatureMeasurementsService getTemperatureMeasurementsService,
            IGetSensorsService getSensorsService, IGetPlacesService getPlacesService,
            IDeleteTemperatureMeasurementService deleteTemperatureMeasurementService,
            ICreateTemperatureMeasurementService createTemperatureMeasurementService,
            IUpdateTemperatureMeasurementService updateTemperatureMeasurementService)
        {
            this.getTemperatureMeasurementsService = getTemperatureMeasurementsService;
            this.getSensorsService = getSensorsService;
            this.getPlacesService = getPlacesService;
            this.deleteTemperatureMeasurementService = deleteTemperatureMeasurementService;
            this.createTemperatureMeasurementService = createTemperatureMeasurementService;
            this.updateTemperatureMeasurementService = updateTemperatureMeasurementService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View("List", new TemperatureMeasurementListViewModel() { CanCreate = User.IsInRole("Admin") });
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var vm = await CreateAndFillViewModelAsync(new TemperatureMeasurementModel(), true);

            return View("Detail", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TemperatureMeasurementModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createTemperatureMeasurementService.CreateAsync(model);
                if (result.ValidationResult.Succeeded)
                {
                    return RedirectToAction("Detail", new { Id = result.Value });
                }

                ModelState.AddValidationErrors(result.ValidationResult);
            }
            
            return View("Detail", await CreateAndFillViewModelAsync(model, true));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            await deleteTemperatureMeasurementService.DeleteAsync(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var temperatureMeasurement = await getTemperatureMeasurementsService.GetTemperatureMeasurementById(id);
            
            var vm = await CreateAndFillViewModelAsync(temperatureMeasurement, false);

            return View("Detail", vm);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(TemperatureMeasurementModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await updateTemperatureMeasurementService.UpdateTemperatureMeasurementAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", new { Id = model.Id });
                }

                ModelState.AddValidationErrors(result);
            }
            
            return View("Detail", await CreateAndFillViewModelAsync(model, false));
        }
        
        public async Task<IActionResult> TemperatureMeasurementsDataSource([FromBody]DataManagerRequest dm)
        {
            var result =
                await getTemperatureMeasurementsService.GetFilteredMeasurementsAsync(new MeasurementFilter(),
                    dm.ToPagingArgs());
            return dm.RequiresCounts ? Json(new { result = result.Items, count = result.Count }) : Json(result.Items);
        }
        
        private async Task<TemperatureMeasurementViewModel> CreateAndFillViewModelAsync(TemperatureMeasurementModel model, bool isCreatePage)
        {
            var viewModel = new TemperatureMeasurementViewModel(model)
            {
                IsCreatePage = isCreatePage,
                CanEdit = User.IsInRole("Admin")
            };

            viewModel.Sensors = await getSensorsService.GetAllSensorsAsync();
            viewModel.Places = await getPlacesService.GetAllPlacesAsync();

            return viewModel;
        }
    }
}