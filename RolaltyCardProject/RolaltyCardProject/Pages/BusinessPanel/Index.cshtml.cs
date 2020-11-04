using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Models.ViewModels;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.CardCreation
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CardCreationViewModel CardCreationViewModel { get; set; }
        public bool IsMaxLoyatyCardsFull { get; set; } = false;
        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole(StaticDetails.BusinessEndUser))
            {
                return RedirectToPage("/RegisterBusinessAccount/Index");
            }

            var userId = EasyMethods.GetUserId(User);

            ICollection<LoyaltyCard> loyaltyCards = await _db.LoyaltyCards.Where(l => l.AplicationUserId == userId).ToListAsync();

            CardCreationViewModel = new CardCreationViewModel
            {
                LoyaltyCards = loyaltyCards,
            };
            return Page();
        }

        public void OnPost()
        {

        }

        public IActionResult OnPostCreate()
        {
            if(CardCreationViewModel.LoyaltyCards.Count < StaticDetails.MaxLoyatyCards)
            {
                return RedirectToPage("Create");
            }
            IsMaxLoyatyCardsFull = true;
            return Page();
        }
    }
}
