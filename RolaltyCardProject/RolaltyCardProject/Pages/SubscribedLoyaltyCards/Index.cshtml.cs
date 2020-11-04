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
                .Include(c => c.LoyaltyCard.AplicationUser)
                .AsNoTracking()
                .Where(c => c.AplicationUserId == userId).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userid, int? cardid)
        {
            if (userid == null || !cardid.HasValue)
            {
                return NotFound("Opps! userid or cardid not found, try again!");
            }

            var card = await _db.ClientCards.FirstOrDefaultAsync(c => c.AplicationUserId == userid && c.LoyaltyCardId == cardid.Value);
            if (card != null)
            {
                _db.ClientCards.Remove(card);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
