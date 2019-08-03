using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpPost]
        public Task<IActionResult> CreateUser(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}