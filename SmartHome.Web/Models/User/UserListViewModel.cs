using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.User
{
    public class UserListViewModel
    {
        public UserListViewModel(IEnumerable<UserModel> items)
        {
            Items = items;
        }

        public IEnumerable<UserModel> Items { get; set; }
    }
}