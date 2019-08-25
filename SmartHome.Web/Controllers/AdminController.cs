using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Admin;

namespace SmartHome.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(long id)
        {
            var model = await userRepository.GetUserAsync(id);
            
            var viewModel = new DetailUserViewModel()
            {
                Model = model
            };

            return View("UserDetail", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserCreate()
        {
            var viewModel = new CreateUserViewModel()
            {
                Model = new CreateUserModel()
            };

            return View("UserCreate", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UserCreate(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userRepository.AddUser(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new { id = (await userRepository.GetUserAsync(model.Email)).Id });
                }
                
                // print errors
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                }
            }

            return View("UserCreate", new CreateUserViewModel {Model = model});
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(long id)
        {
            var model = await userRepository.GetUserAsync(id);
            
            var vm = new ChangePasswordViewModel() { Model = new ChangePasswordModel() { Id = model.Id }};
            return View("ChangePassword", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userRepository.ChangePasswordAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new {id = model.Id});
                }
                
                ModelState.AddModelError("Error", "An error has occured.");
            }

            return View("ChangePassword", new ChangePasswordViewModel() {Model = model});
        }
    }
}