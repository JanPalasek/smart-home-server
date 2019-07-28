using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Overview()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public async Task<IActionResult> BatterySourceType(long id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public async Task<IActionResult> BatterySourceType()
        {
            throw new NotImplementedException();
        }
    }
}