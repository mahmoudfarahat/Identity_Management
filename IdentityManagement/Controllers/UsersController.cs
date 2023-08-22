using IdentityManagement.Models;
using IdentityManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            //var users = await userManager.Users.Select(user => new UserViewModel
            //{ 
            //Id= user.Id,
            //FirstName = user.FirstName,
            //LastName = user.LasttName,
            //Email = user.Email,
            ////Roles = userManager.GetRolesAsync(user).Result 
            //  Roles= userManager.GetRolesAsync(user).GetAwaiter().GetResult()
            //}).ToListAsync();

            //return View(users);

            var users = await userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LasttName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                };

                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> ManageRoles()
        {

        }


    }
}
