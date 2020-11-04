using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Data
{
    //This class is intended to remove repeated queries and reduce code size.
    //Note: this was added later on, so some there will be some queries that still can be added here.
    public class CommonQuery
    {
        public static async Task<ICollection<ApplicationUser>> GetUsersSearchRoleAsync(ApplicationDbContext db, UserManager<IdentityUser> userManager, string search, string role)
        {
            ICollection<ApplicationUser> users;
            if (string.IsNullOrEmpty(search))
            {
                users = await db.AplicationUsers
                                 .AsNoTracking()
                                 .ToListAsync();
            }
            else
            {
                users = await db.AplicationUsers
                    .AsNoTracking()
                    .Where(u =>
                        u.UserName.Contains(search) ||
                        u.Cpf == search ||
                        u.Email.Contains(search) ||
                        u.CompanyName.Contains(search) ||
                        u.PhoneNumber.Contains(search) ||
                        u.City.Contains(search)
                    ).ToListAsync();
            }
            return users.Where(u => userManager.IsInRoleAsync(user: u, role).GetAwaiter().GetResult()).ToList();
        }

        public static async Task<ApplicationUser> GetApplicationUserAsync(ApplicationDbContext db, string id)
        {
            return await db.AplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
