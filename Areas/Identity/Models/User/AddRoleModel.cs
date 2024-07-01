using System.Collections.Generic;
using aspnetcoremvc.Models;

namespace aspnetcoremvc.Areas.Identity.Models.UserViewModels
{
    public class AddRoleModel
    {
        public string roleId { get; set; } = string.Empty;
        public string roleName { get; set; } = string.Empty;

        public bool IsSelected { get; set; }

    }

}