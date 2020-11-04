using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Models;
using RolaltyCardProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManage)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManage;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
            if (_roleManager.Roles.ToList().Count > 0) return;

            _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdminEndUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(StaticDetails.BusinessEndUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(StaticDetails.ClientEndUser)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            }, "Admin123*").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.UserName == "Admin").GetAwaiter().GetResult(), StaticDetails.AdminEndUser).GetAwaiter().GetResult();
        }
    }
}
