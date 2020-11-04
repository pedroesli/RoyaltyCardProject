using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RolaltyCardProject.Attributes;
using RolaltyCardProject.Data;
using RolaltyCardProject.Models;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.AdminPanel.Admins
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class InputModel
        {
            [Required]
            [Display(Name ="User Name")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [Cpf]
            public string Cpf { get; set; }
            [Required]
            [Display(Name ="Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [Required]
            public string City { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string Message { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var admin = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    Cpf = Input.Cpf,
                    PhoneNumber = Input.PhoneNumber,
                    City = Input.City
                };
                var result = await _userManager.CreateAsync(admin, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(admin.UserName), StaticDetails.AdminEndUser);
                    Message = "New admin created successfuly";
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
