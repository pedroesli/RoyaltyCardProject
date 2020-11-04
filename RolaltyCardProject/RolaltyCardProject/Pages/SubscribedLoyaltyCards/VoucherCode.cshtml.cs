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

namespace RolaltyCardProject.Pages.SubscribedLoyaltyCards
{
    [Authorize]
    public class VoucherCodeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public VoucherCodeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public ClientCards ClientCard { get; set; }

        public async Task<IActionResult> OnGetAsync(string userid, int? cardid)
        {
            if (userid == null || !cardid.HasValue)
            {
                return NotFound("Error, userid or cardid not found!");
            }
            ClientCard = new ClientCards();

            var card = await _db.ClientCards
                .Include(c => c.LoyaltyCard)
                .FirstOrDefaultAsync(c => c.AplicationUserId == userid && c.LoyaltyCardId == cardid.Value);
            
            //Generate a voucher code if the points has reached the total points required
            if (card.Points == card.LoyaltyCard.TotalPoints && card.VoucherCode == null)
            {
                card.VoucherCode = new VoucherCode().Generate();
                await _db.SaveChangesAsync();
            }

            ClientCard = card;
            return Page();
        }
    }
}
