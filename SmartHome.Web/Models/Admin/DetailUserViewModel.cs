using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Admin
{
    public class DetailUserViewModel
    {
        public List<RoleModel> AvailableRoles { get; set; }
        
        public UserModel Model { get; set; }
        public List<long> Roles { get; set; }
        
        public DetailUserViewModel(UserModel model, List<long> roles, List<RoleModel> availableRoles)
        {
            Model = model;
            Roles = roles;
            AvailableRoles = availableRoles;
        }
    }
}