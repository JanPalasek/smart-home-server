using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Admin;
using SmartHome.Web.Models.Admin;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICreateUserService createUserService;
        private readonly IGetUsersService getUsersService;
        private readonly IChangePasswordService changePasswordService;

        public AdminController(ICreateUserService createUserService, IGetUsersService getUsersService,
            IChangePasswordService changePasswordService)
        {
            this.createUserService = createUserService;
            this.getUsersService = getUsersService;
            this.changePasswordService = changePasswordService;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(long id)
        {
            var model = await getUsersService.GetByIdAsync(id);
            
            var viewModel = new DetailUserViewModel(model);

            return View("UserDetail", viewModel);
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            var viewModel = new CreateUserViewModel(new CreateUserModel());

            return View("UserCreate", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UserCreate(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createUserService.CreateUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new { id = (await getUsersService.GetByEmailAsync(model.Email!))!.Id });
                }
                
                // print errors
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                }
            }

            return View("UserCreate", new CreateUserViewModel(model));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(long id)
        {
            var model = await getUsersService.GetByIdAsync(id);
            
            var vm = new ChangePasswordViewModel(new ChangePasswordModel() { Id = model.Id });
            return View("ChangePassword", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await changePasswordService.ChangePasswordAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new {id = model.Id});
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View("ChangePassword", new ChangePasswordViewModel(model));
        }
    }
}