using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;

namespace RolaltyCardProject.Pages.AdminPanel.Common
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser AppUser { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }
        public async Task OnGetAsync(string id, string returnUrl)
        {
            AppUser = await CommonQuery.GetApplicationUserAsync(_db, id);
            ReturnUrl = returnUrl;
        }
    }
}
