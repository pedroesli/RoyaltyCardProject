using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.AdminPanel.RoyaltyCards
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public ICollection<LoyaltyCard> LoyaltyCards { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public async Task OnGetAsync()
        {
            ICollection<LoyaltyCard> cards;
            if (string.IsNullOrEmpty(Search))
            {
                cards = await _db.LoyaltyCards.Include(c => c.AplicationUser).ToListAsync();
            }
            else
            {
                cards = await _db.LoyaltyCards
                    .Include(c => c.AplicationUser)
                    .AsNoTracking()
                    .Where(c =>
                        c.CardName.Contains(Search) ||
                        c.Header.Contains(Search) ||
                        c.AplicationUser.UserName.Contains(Search)
                    ).ToListAsync();
            }
            LoyaltyCards = cards;
        }
    }
}
