using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class StatisticsController : Controller
    {
        [HttpGet("statistics")]
        public async Task<IActionResult> Index()
        {
            return View("Statistics", null);
        }
    }
}