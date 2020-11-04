using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.AdminPanel.ClientUsers
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public ICollection<ApplicationUser> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public async Task OnGetAsync()
        {
            Users = await CommonQuery.GetUsersSearchRoleAsync(_db, _userManager, Search, StaticDetails.ClientEndUser);
        }
    }
}
