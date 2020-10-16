using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models.ViewModels
{
    public class CardCreationViewModel
    {
        public ICollection<LoyaltyCard> LoyaltyCards { get; set; }
    }
}
