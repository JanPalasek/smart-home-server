using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Role;
using SmartHome.DomainCore.ServiceInterfaces.User;
using SmartHome.Web.Models.Role;
using SmartHome.Web.Models.User;

namespace SmartHome.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly ICreateUserService createUserService;
        private readonly IGetUsersService getUsersService;
        private readonly IUpdateUserService updateUserService;
        private readonly IDeleteUserService deleteUserService;
        private readonly IChangePasswordService changePasswordService;
        private readonly IGetRolesService getRolesService;

        public UserController(ICreateUserService createUserService, IGetUsersService getUsersService,
            IUpdateUserService updateUserService, IChangePasswordService changePasswordService,
            IGetRolesService getRolesService, IDeleteUserService deleteUserService)
        {
            this.createUserService = createUserService;
            this.getUsersService = getUsersService;
            this.updateUserService = updateUserService;
            this.changePasswordService = changePasswordService;
            this.getRolesService = getRolesService;
            this.deleteUserService = deleteUserService;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(long id)
        {
            // TODO: validate that if User doesn't have role "Admin", he only can view his own
            var model = await getUsersService.GetByIdAsync(id);

            var availableRoles = await getRolesService.GetAllRolesAsync();
            var userRoles = await getRolesService.GetUserRolesAsync(id);
            
            var viewModel = new DetailUserViewModel(model, userRoles.Select(x => x.Id).ToList(),
                (List<RoleModel>)availableRoles);

            return View("UserDetail", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserModel model, List<long> roles)
        {
            // TODO: validate that if User doesn't have role "Admin", he only can update his own
            if (ModelState.IsValid)
            {
                var result = await updateUserService.AddToOrRemoveFromRolesAsync(model.Id, roles);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new {id = model.Id});
                }
                
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                }
            }
            
            var availableRoles = await getRolesService.GetAllRolesAsync();
            return View("UserDetail", new DetailUserViewModel(model, roles,
                (List<RoleModel>)availableRoles));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList()
        {
            var users = await getUsersService.GetAllUsersAsync();

            return View("UserList", new UserListViewModel(users));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UserCreate()
        {
            var viewModel = new CreateUserViewModel(new CreateUserModel());

            return View("UserCreate", viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserDelete(long id)
        {
            // TODO: validate in service that he can't delete his own
            var result = await deleteUserService.DeleteUserAsync(id);

            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            
            throw new ArgumentException("User cannot be deleted.");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(long id)
        {
            // TODO: no admin => can change only his own
            var model = await getUsersService.GetByIdAsync(id);
            
            var vm = new ChangePasswordViewModel(new ChangePasswordModel() { Id = model.Id });
            return View("ChangePassword", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            // TODO: no admin => can change only his own
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