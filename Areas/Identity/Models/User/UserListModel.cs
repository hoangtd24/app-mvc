using System.Collections.Generic;
using aspnetcoremvc.Models;

namespace aspnetcoremvc.Areas.Identity.Models.UserViewModels
{
    public class UserListModel
    {
        public int totalUsers { get; set; }
        public int countPages { get; set; }

        public int ITEMS_PER_PAGE { get; set; } = 10;

        public int currentPage { get; set; }

        public List<UserAndRole> users { get; set; } = new List<UserAndRole>();

    }

    public class UserAndRole : AppUser
    {
        public string RoleNames { get; set; } = string.Empty;
    }


}