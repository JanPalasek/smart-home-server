using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;
using SmartHome.Web.Models.Statistics;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Charts;

namespace SmartHome.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class StatisticsController : Controller
    {
        private readonly IGetTemperatureMeasurementsService getTemperatureMeasurementsService;

        public StatisticsController(IGetTemperatureMeasurementsService getTemperatureMeasurementsService)
        {
            this.getTemperatureMeasurementsService = getTemperatureMeasurementsService;
        }

        [HttpGet("Statistics")]
        public async Task<IActionResult> Index()
        {
            var vm = new StatisticsViewModel();
            vm.Items = await getTemperatureMeasurementsService.GetFilteredMeasurementsAsync(new MeasurementFilter());
            return View("Statistics", vm);
        }
        
        public async Task<IActionResult> TemperaturesDataSource([FromBody]DataManagerRequest dm)
        {
            var result = (await getTemperatureMeasurementsService.GetFilteredMeasurementsAsync(new MeasurementFilter()))
                .Select(x => new {  x.MeasurementDateTime, x.Temperature })
                .ToList();
            return dm.RequiresCounts ? Json(new { result = result, count = result.Count }) : Json(result);
        }
        
        public async Task<IActionResult> TemperaturesDataSourceNoParam()
        {
            var result = (await getTemperatureMeasurementsService.GetFilteredMeasurementsAsync(new MeasurementFilter()))
                .Select(x => new {  x.MeasurementDateTime, x.Temperature })
                .ToList();
            return Json(new {result = result, count = result.Count});
        }
    }
}