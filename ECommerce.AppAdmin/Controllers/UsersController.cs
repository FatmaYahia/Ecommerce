using ECommerce.AppAdmin.Models;
using ECommerce.AppAdmin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.AppAdmin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task< IActionResult> Index()
        {
            var data= await _userManager.Users.ToListAsync();
            var users = data.Select(
               x=>new UserVM()
               {
                   Id=x.Id,
                   FirstName=x.FirstName,
                   LastName=x.LastName,
                   UserName=x.UserName,
                   Email=x.Email,
                   Roles=_userManager.GetRolesAsync(x).Result
               } 
                
                ).ToList();
            return View(users);
        }
        public async Task<IActionResult> manageRoles(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles =await _roleManager.Roles.ToListAsync();
            UserRolesVM userRoles = new UserRolesVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                
                Roles =  roles.Select(role =>
                new RoleVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    isSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }

                ).ToList()
            };
            return View(userRoles);
        }
        [HttpPost]
        public async Task<IActionResult> manageRoles(UserRolesVM userRolesVM)
        {
            var user =await _userManager.FindByIdAsync(userRolesVM.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var olduserRoles =await _userManager.GetRolesAsync(user);
           
            foreach(var role in  userRolesVM.Roles)
            {
                if (olduserRoles.Any(x => x == role.RoleName) && !role.isSelected)
                   await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (!olduserRoles.Any(x => x == role.RoleName) && role.isSelected)
                   await _userManager.AddToRoleAsync(user, role.RoleName);

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
