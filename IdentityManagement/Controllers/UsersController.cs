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

       public async Task<IActionResult> Add()
        {
            var roles = await roleManager.Roles.Select( r => new RoleViewModel
            {
                RoleName= r.Name
            }).ToListAsync();

            var viewModel = new AddUserViewModel
            {
                Roles = roles
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please select at least one role");
                return View(model);
            }
            if (await userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already exists");
                return View(model);
            }
            if (await userManager.FindByNameAsync(model.Username) != null)
            {
                ModelState.AddModelError("Username", "Username is already exists");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName =model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LasttName = model.LastName
            };


            var result = await userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }

            await userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var viewModel = new ProfileFormViewModel
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LasttName,
                Username = user.UserName,
                Email = user.Email
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();
            var userWithSameEmail = await userManager.FindByEmailAsync(model.Email);
            if(userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "this email is already assignet to anther user");
                return View(model);
            }
            var userWithSameUsername = await userManager.FindByNameAsync(model.Username);
            if (userWithSameUsername != null && userWithSameUsername.Id != model.Id)
            {
                ModelState.AddModelError("Email", "this username is already assignet to anther user");
                return View(model);
            }
            user.FirstName = model.FirstName;
            user.LasttName = model.LastName;
            user.UserName = model.Username;
            user.Email = model.Email;

            await userManager.UpdateAsync(user);


            return RedirectToAction(nameof(Index));
        }
    }
}
