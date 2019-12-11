using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartHome.Common.Extensions;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.DomainCore.ServiceInterfaces.Role;
using SmartHome.DomainCore.ServiceInterfaces.User;
using SmartHome.Web.Models.Role;
using SmartHome.Web.Models.User;
using SmartHome.Web.Utils;
using Syncfusion.EJ2.Base;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICreateUserService createUserService;
        private readonly IGetUsersService getUsersService;
        private readonly IUpdateUserService updateUserService;
        private readonly IDeleteUserService deleteUserService;
        private readonly IChangePasswordService changePasswordService;
        private readonly IGetRolesService getRolesService;
        private readonly IGetPermissionsService getPermissionsService;

        public UserController(ICreateUserService createUserService, IGetUsersService getUsersService,
            IUpdateUserService updateUserService, IChangePasswordService changePasswordService,
            IGetRolesService getRolesService, IDeleteUserService deleteUserService, IGetPermissionsService getPermissionsService)
        {
            this.createUserService = createUserService;
            this.getUsersService = getUsersService;
            this.updateUserService = updateUserService;
            this.changePasswordService = changePasswordService;
            this.getRolesService = getRolesService;
            this.deleteUserService = deleteUserService;
            this.getPermissionsService = getPermissionsService;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(long id)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(id))
            {
                return Unauthorized();
            }
            
            var model = await getUsersService.GetByIdAsync(id);

            var availableRoles = await getRolesService.GetAllRolesAsync();
            var userRoles = await getRolesService.GetUserRolesAsync(id);

            var permissions = await getPermissionsService.GetAllPermissionsAsync(id);
            
            var viewModel = new DetailUserViewModel(model, userRoles.Select(x => x.Id).ToList(),
                (List<RoleModel>)availableRoles, (List<PermissionRoleModel>)permissions)
            {
                CanDelete = User.Identity.Name != model.UserName,
                CanEditRoles = User.IsInRole("Admin")
            };

            return View("UserDetail", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserModel model, List<long> roles)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(model.Id))
            {
                return Unauthorized();
            }
            
            if (ModelState.IsValid)
            {
                // if user attempts to update roles and is not admin => error
                var storedRoles = (await getRolesService.GetUserRolesAsync(model.Id)).Select(x => x.Id).ToList();
                if (!storedRoles.UnorderedEquals(roles) && !User.IsInRole("Admin"))
                {
                    return Unauthorized();
                }
                
                var result = await updateUserService.AddToOrRemoveFromRolesAsync(model.Id, roles);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new {id = model.Id});
                }
                
                ModelState.AddValidationErrors(result);
            }
            
            var availableRoles = await getRolesService.GetAllRolesAsync();
            var permissions = await getPermissionsService.GetAllPermissionsAsync(model.Id);
            
            return View("UserDetail", new DetailUserViewModel(model, roles,
                (List<RoleModel>)availableRoles, (List<PermissionRoleModel>)permissions)
            {
                CanDelete = User.Identity.Name != model.UserName,
                CanEditRoles = User.IsInRole("Admin")
            });
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
                
                ModelState.AddValidationErrors(result);
            }

            return View("UserCreate", new CreateUserViewModel(model));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserDelete(long id)
        {
            // cannot delete your own account
            if ((await getUsersService.GetByNameAsync(User.Identity.Name!))?.Id == id)
            {
                return Unauthorized();
            }
            
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
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(id))
            {
                return Unauthorized();
            }
            
            var model = await getUsersService.GetByIdAsync(id);
            
            var vm = new ChangePasswordViewModel(new ChangePasswordModel() { Id = model.Id });
            return View("ChangePassword", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(model.Id))
            {
                return Unauthorized();
            }
            
            if (ModelState.IsValid)
            {
                var result = await changePasswordService.ChangePasswordAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new {id = model.Id});
                }

                ModelState.AddValidationErrors(result);
            }

            return View("ChangePassword", new ChangePasswordViewModel(model));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Permissions(long userId)
        {
            var vm = new UserPermissionsViewModel(userId);
            vm.PermissionOptions = await getPermissionsService.GetAllPermissionsAsync();
            return View("Permissions", vm);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PermissionsDataSource(long userId, [FromBody]DataManagerRequest dm)
        {
            var result =
                await getPermissionsService.GetUserOnlyPermissionsAsync(userId);
            return dm.RequiresCounts ? Json(new { result = result, count = result.Count }) : Json(result);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PermissionsUpdate(long userId, [FromBody]CRUDModel batchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var removed =  batchModel.Deleted.Count > 0 ? batchModel.Deleted.Select(x => JsonConvert.DeserializeObject<PermissionModel>(x.ToString()))
                .Select(x => x.Id) : Enumerable.Empty<long>();
            var updated = batchModel.Changed.Count > 0 ? batchModel.Changed.Select(x => JsonConvert.DeserializeObject<PermissionModel>(x.ToString()))
                .Select(x => (x.Id, x.Name)) : Enumerable.Empty<ValueTuple<long, string>>();
            var added = batchModel.Added.Select(x => JsonConvert.DeserializeObject<PermissionModel>(x.ToString()))
                .Select(x => x.Name).ToList();

            var result = await updateUserService.UpdatePermissionsAsync(userId, removed, updated, added);
            
            
            var permissions =
                await getPermissionsService.GetUserOnlyPermissionsAsync(userId);
            
            return Json(permissions);
        }
            
        private async Task<bool> IsAuthorizedToChangeUserSettings(long userId)
        {
            if (User.Identity.Name == null)
            {
                return false;
            }
            if (!User.IsInRole("Admin") &&
                (await getUsersService.GetByNameAsync(User.Identity.Name))?.Id != userId)
            {
                return false;
            }

            return true;
        }
    }
}