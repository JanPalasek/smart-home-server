using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public Task<IActionResult> Overview()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public Task<IActionResult> BatterySourceType(long id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public Task<IActionResult> BatterySourceType()
        {
            throw new NotImplementedException();
        }
    }
}