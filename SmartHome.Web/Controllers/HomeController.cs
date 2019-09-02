using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Overview()
        {
            return View("Overview");
        }
    }
}