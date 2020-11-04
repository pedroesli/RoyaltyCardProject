using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Models.ViewModels;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.BusinessPanel
{
    public class RedeemCodeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public RedeemCodeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public RedeemCodeViewModel RedeemCodeViewModel { get; set; }
        public void OnGet()
        {
            RedeemCodeViewModel = new RedeemCodeViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(RedeemCodeViewModel.Code))
                return Page();

            var userId = EasyMethods.GetUserId(User);
            var businessCards = await _db.LoyaltyCards
                .AsNoTracking()
                .Where(l => l.AplicationUserId == userId)
                .ToListAsync();
            if (businessCards != null)
            {
                //find the client card with the voucher code where the Loyalty card belongs to this business user, so that the user can't redeem codes from other business users
                var clientCard = await _db.ClientCards.FirstOrDefaultAsync(c => c.VoucherCode == RedeemCodeViewModel.Code);
                if (clientCard != null && businessCards.Exists(b => b.Id == clientCard.LoyaltyCardId))
                {
                    if (!clientCard.IsRedeemed)
                    {
                        clientCard.IsRedeemed = true;
                        await _db.SaveChangesAsync();
                        RedeemCodeViewModel.Message = new Message
                        {
                            MessageType = Message.Type.Successful,
                            Text = "Card has been redeemed, give the client his god dam prize! :)"
                        };
                    }
                    else
                    {
                        //Card has already been redeemed
                        RedeemCodeViewModel.Message = new Message
                        {
                            MessageType = Message.Type.Info,
                            Text = "Card has already been redeemed"
                        };
                    }
                }
                else
                {
                    //Could not find card
                    RedeemCodeViewModel.Message = new Message
                    {
                        MessageType = Message.Type.Error,
                        Text = "Could not find this voucher code"
                    };
                }
            }
            else
            {
                RedeemCodeViewModel.Message = new Message
                {
                    MessageType = Message.Type.Info,
                    Text = "You don't have any Loyalty Cards!"
                };
            }
            return Page();
        }
    }
}
