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

namespace RolaltyCardProject.Pages.CardCreation
{
    public class AddPointModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AddPointModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public AddPointViewModel AddPointViewModel { get; set; }
        public IActionResult OnGet(int? id, string name = "")
        {
            if (id == null)
                return NotFound("Can not load page without Loyalty Card id. Select a Loyalty Card from card creation.");

            AddPointViewModel = new AddPointViewModel
            {
                ClientCardName = name
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string name = "")
        {
            if (id == null)
                return NotFound();

            AddPointViewModel.ClientCardName = name;

            if (ModelState.IsValid)
            {
                var user = await _db.AplicationUsers.FirstOrDefaultAsync(u => u.Cpf == AddPointViewModel.Cpf);
                if (user != null)
                {
                    AddPointViewModel.ClientCard = await _db.ClientCards
                        .Include(u => u.LoyaltyCard)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.AplicationUserId == user.Id && u.LoyaltyCardId == id.Value);
                    if (AddPointViewModel.ClientCard != null)
                    {
                        AddPointViewModel.ClientCard.AplicationUser = user;
                    }
                    else
                    {
                        //Subscribe user to card and give point
                        ClientCards clientCards = new ClientCards
                        {
                            AplicationUserId = user.Id,
                            LoyaltyCardId = id.Value,
                            Points = 1
                        };
                        await _db.ClientCards.AddAsync(clientCards);
                        await _db.SaveChangesAsync();

                        AddPointViewModel.Message = new Message
                        {
                            MessageType = Message.Type.Successful,
                            Text = "User has been subcribed to this Loyalty Card and has been given a point"
                        };
                    }
                }
                else
                {
                    AddPointViewModel.Message = new Message
                    {
                        MessageType = Message.Type.Error,
                        Text = "Could not Find the user with the Cpf: " + AddPointViewModel.Cpf
                    };
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(int? id, string userId, string name = "")
        {
            if (id == null || userId == null)
                return NotFound("An error has occured, try again.");

            var clientCard = await _db.ClientCards
                .Include(c => c.LoyaltyCard)
                .FirstOrDefaultAsync(c => c.LoyaltyCardId == id.Value && c.AplicationUserId == userId);
            if (clientCard.Points < clientCard.LoyaltyCard.TotalPoints)
            {
                clientCard.Points++;
                await _db.SaveChangesAsync();

                AddPointViewModel.Message = new Message
                {
                    MessageType = Message.Type.Successful,
                    Text = "Point has been given to user"
                };
            }
            return RedirectToPage("AddPoint", new { id = id.Value, name });
        }
    }
}
