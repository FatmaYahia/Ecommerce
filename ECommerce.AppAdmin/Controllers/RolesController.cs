using ECommerce.AppAdmin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.AppAdmin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManger;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManger = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            RoleDataVM data = new RoleDataVM()
            {
                identityRole = await _roleManger.Roles.ToListAsync(),
                roleFormViewModel = new RoleFormViewModel()
            };
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDataVM item)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new RoleDataVM()
                {
                    identityRole = await _roleManger.Roles.ToListAsync(),
                    roleFormViewModel = new RoleFormViewModel()
                });
            }
            string roleName = item.roleFormViewModel.Name;
            var roleExists = await _roleManger.RoleExistsAsync(roleName);
            if (roleExists)
            {
                ModelState.AddModelError("roleFormViewModel.Name", "This role is already exists");
                return View("Index", new RoleDataVM()
                {
                    identityRole = await _roleManger.Roles.ToListAsync(),
                    roleFormViewModel = new RoleFormViewModel()
                });

            }
            await  _roleManger.CreateAsync(new IdentityRole( roleName.Trim()));
           
           return RedirectToAction(nameof(Index));
        }

    }
}



