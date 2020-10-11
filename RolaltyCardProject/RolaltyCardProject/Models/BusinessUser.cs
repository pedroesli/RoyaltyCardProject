using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolaltyCardProject.Models
{
    public class BusinessUser : IdentityUser
    {
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public Country Country { get; set; }
        public ICollection<LoyaltyCard> LoyaltyCards { get; set; }
    }

}
