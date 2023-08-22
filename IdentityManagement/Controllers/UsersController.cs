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

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return NotFound();
            }
            var  roles = await roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                Roles =    roles.Select(role =>  new RoleViewModel
                {
                    RoleName = role.Name,
                    IsSelected =   userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user is null)
                return NotFound();
            //foreach (var role in model.Roles)
            //{
            //    if(!await userManager.IsInRoleAsync(user, role.RoleName) && role.IsSelected)
            //    {
            //        await userManager.AddToRoleAsync(user, role.RoleName);
            //    }

            //    if (await userManager.IsInRoleAsync(user, role.RoleName) && !role.IsSelected)
            //    {
            //        await userManager.RemoveFromRoleAsync(user, role.RoleName);
            //    }
            //}

            //return View(model);

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if(userRoles.Any(r=>r == role.RoleName) && !role.IsSelected)
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);
               if (!userRoles.Any(r => r == role.RoleName) &&  role.IsSelected)
                    await userManager.AddToRoleAsync(user, role.RoleName);

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
