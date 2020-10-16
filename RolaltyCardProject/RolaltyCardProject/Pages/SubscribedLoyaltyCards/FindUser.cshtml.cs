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

namespace RolaltyCardProject.Pages.SubscribedLoyaltyCards
{
    public class FindUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public FindUserModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public FindUserViewModel FindUserViewModel { get; set; }
        public void OnGet()
        {
            FindUserViewModel = new FindUserViewModel();
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _db.AplicationUsers.FirstOrDefaultAsync(u => u.Cpf == FindUserViewModel.Cpf);
                if (user != null)
                {
                    var clientCards = await _db.ClientCards
                        .Include(c => c.LoyaltyCard)
                        .AsNoTracking()
                        .Where(c => c.AplicationUserId == user.Id)
                        .ToListAsync();

                    if(clientCards != null)
                    {
                        FindUserViewModel.ClientCards = clientCards;
                    }
                    else
                    {
                        FindUserViewModel.Message = new Message
                        {
                            MessageType = Message.Type.Info,
                            Text = "This user does not have any Loyalty Cards"
                        };
                    }
                }
                else
                {
                    FindUserViewModel.Message = new Message
                    {
                        MessageType = Message.Type.Error,
                        Text = "Could not find a user with this Cpf"
                    };
                }
            }
        }
    }
}
