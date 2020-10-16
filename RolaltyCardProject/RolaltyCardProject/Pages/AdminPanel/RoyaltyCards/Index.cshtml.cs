using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RolaltyCardProject.Utility;

namespace RolaltyCardProject.Pages.AdminPanel.RoyaltyCards
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
