using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.SubscribedLoyaltyCards
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<ClientCards> ClientCards { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("FindUser");
            }

            var userId = EasyMethods.GetUserId(User);
            ClientCards = await _db.ClientCards
                .Include(c => c.LoyaltyCard)
                .AsNoTracking()
                .Where(c => c.AplicationUserId == userId).ToListAsync();
            return Page();
        }
    }
}
