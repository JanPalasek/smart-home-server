using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create()
        {
            var vm = await CreateAndFillViewModelAsync(new TemperatureMeasurementModel(), true);

            return View("Detail", vm);
        }

        [HttpPost]
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
            var dataSource = await getTemperatureMeasurementsService.GetAllAsync();
//            DataOperations operation = new DataOperations();
//            if (dm.Search != null && dm.Search.Count > 0)
//            {
//                dataSource = operation.PerformSearching(dataSource, dm.Search);  //Search
//            }
//            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
//            {
//                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
//            }
//            if (dm.Where != null && dm.Where.Count > 0) //Filtering
//            {
//                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
//            }
//            int count = dataSource.Cast<TemperatureMeasurementModel>().Count();
//            if (dm.Skip != 0)
//            {
//                dataSource = operation.PerformSkip(dataSource, dm.Skip);         //Paging
//            }
//            if (dm.Take != 0)
//            {
//                dataSource = operation.PerformTake(dataSource, dm.Take);
//            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = dataSource.Count }) : Json(dataSource);
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