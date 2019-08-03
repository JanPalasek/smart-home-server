using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Account;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository repository;

        public AccountController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.SignInAsync(model);
                if (result?.Succeeded == true)
                {
                    return RedirectToAction(returnUrl);
                }
            }

            return View("Login", new LoginViewModel() { Model = model, ReturnUrl = returnUrl });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            return View("Login", new LoginViewModel() { ReturnUrl = returnUrl });
        }
    }
}