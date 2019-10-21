// I, Neel Thakkar, student number 000743545, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab01B.Data;
using Lab01B.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab01B.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> RoleManager)
        {
            _userManager = userManager;
            _roleManager = RoleManager;
        }
        [HttpGet]
        public async Task<IActionResult> SeedRoles()
        {
            ApplicationUser user1 = new ApplicationUser { Email = "jack@ryan.com", UserName = "jack@ryan.com", FirstName = "Jack", LastName = "Ryan", BirthDate = DateTime.Now, Company = "CIA", Position = "Analyst" };
            ApplicationUser user2 = new ApplicationUser { Email = "manoj@bajpai.com", UserName = "manoj@bajpai.com", FirstName = "Manoj", LastName = "Bajpai", BirthDate = DateTime.Now, Company = "NSA", Position = "Senior Analyst" };

            IdentityResult result = await _userManager.CreateAsync(user1, "Password1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _userManager.CreateAsync(user2, "Password1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new Roles" });

            result = await _roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new Roles" });

            result = await _userManager.AddToRoleAsync(user1, "Supervisor");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            result = await _userManager.AddToRoleAsync(user2, "Employee");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            return RedirectToAction("Index", "Home");
        }

    }
}