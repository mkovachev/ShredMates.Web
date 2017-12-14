using Microsoft.AspNetCore.Mvc.Rendering;
using ShredMates.Services.Admin.Models;
using System.Collections.Generic;

namespace ShredMates.Web.Areas.Admin.Models.Users
{
    public class AllUsersViewModel
    {
        public IEnumerable<AdminUserServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
