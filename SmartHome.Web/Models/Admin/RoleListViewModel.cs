using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Admin
{
    public class RoleListViewModel
    {
        public RoleListViewModel(IEnumerable<RoleModel> items)
        {
            Items = items;
        }

        public IEnumerable<RoleModel> Items { get; set; }
        
    }
}