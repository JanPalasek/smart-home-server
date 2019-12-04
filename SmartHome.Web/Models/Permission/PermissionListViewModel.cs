using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Permission
{
    public class PermissionListViewModel
    {
        public PermissionListViewModel(IEnumerable<PermissionModel> items)
        {
            Items = items;
        }

        public bool CanCreate { get; set; }
        public IEnumerable<PermissionModel> Items { get; set; }
    }
}