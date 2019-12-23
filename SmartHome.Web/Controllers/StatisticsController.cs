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
        
        [HttpGet]
        public async Task<IActionResult> StatisticsDataSource([Bind(Prefix = "")]StatisticsFilter statisticsFilter)
        {
            var filteredMeasurements = await getTemperatureMeasurementsService
                .GetFilteredMeasurementAsync(statisticsFilter);

            return Json(filteredMeasurements);
        }
    }
}