using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
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
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.SignInAsync(model);
                if (result?.Succeeded == true && returnUrl != null)
                {
                    return Redirect(returnUrl);
                }

                if (result?.Succeeded == true)
                {
                    // redirect to base page
                    return Redirect("/");
                }
                
                ModelState.AddModelError(nameof(model.Login), "Invalid credentials.");
            }

            return View("Login", new LoginViewModel(model) { ReturnUrl = returnUrl });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            return View("Login", new LoginViewModel(new LoginModel()) { ReturnUrl = returnUrl });
        }
    }
}