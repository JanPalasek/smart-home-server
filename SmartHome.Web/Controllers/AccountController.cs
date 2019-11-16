using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Account;
using SmartHome.DomainCore.ServiceInterfaces.Admin;
using SmartHome.Web.Models.Account;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ISignInService signInService;
        private readonly ISignOutService signOutService;
        private readonly IGetUsersService getUsersService;

        public AccountController(ISignInService signInService,
            ISignOutService signOutService,
            IGetUsersService getUsersService)
        {
            this.signInService = signInService;
            this.signOutService = signOutService;
            this.getUsersService = getUsersService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInService.SignInAsync(model);
                if (result.Succeeded && returnUrl != null)
                {
                    return Redirect(returnUrl);
                }

                if (result.Succeeded)
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

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signOutService.SignOutAsync();
            return RedirectToAction("Overview", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await getUsersService.GetByNameAsync(User.Identity.Name!);
            
            return RedirectToAction("ChangePassword", "User", new {id = user!.Id});
        }
    }
}