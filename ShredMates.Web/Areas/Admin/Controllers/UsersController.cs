﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data.Models;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Web.Areas.Admin.Models.Users;
using ShredMates.Web.Infrastructure.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace ShredMates.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();
            var roles = await this.roleManager
                            .Roles
                            .Select(r => new SelectListItem
                            {
                                Text = r.Name,
                                Value = r.Name
                            })
                            .ToListAsync();

            return View(new AllUsersViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddtoRole(AddUserToRoleViewModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);

            if (user == null || !roleExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var userisInRole = await userManager.IsInRoleAsync(user, model.Role);

            if (userisInRole)
            {
                TempData.AddErrorMessage($"{user.UserName} already has role {model.Role}!");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await this.userManager.AddToRoleAsync(user, model.Role);

                TempData.AddSuccessMessage($"User {user.UserName} successfully added to role {model.Role}.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}