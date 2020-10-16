using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.CardCreation
{
    [Authorize(Roles = StaticDetails.BusinessEndUser)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public LoyaltyCard LoyaltyCard { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userId = EasyMethods.GetUserId(User);
                LoyaltyCard.AplicationUserId = userId;

                _db.LoyaltyCards.Add(LoyaltyCard);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
