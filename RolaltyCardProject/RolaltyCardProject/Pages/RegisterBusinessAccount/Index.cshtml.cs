using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.RegisterBusinessAccount
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IndexModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public IActionResult OnGet()
        {
            if (User.IsInRole(StaticDetails.BusinessEndUser))
                return RedirectToPage("../Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                var user = await _db.AplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
                if(user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, StaticDetails.ClientEndUser);
                    await _userManager.AddToRoleAsync(user, StaticDetails.BusinessEndUser);
                    user.CompanyName = Input.CompanyName;
                    user.PhoneNumber = Input.PhoneNumber;
                    await _db.SaveChangesAsync();
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToPage("/CardCreation/Index");
                }
            }
            return Page();
        }

    }
}
