using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models
{
    public class AplicationUser : IdentityUser
    {
        [Required]
        public string Cpf { get; set; }
        public ICollection<ClientCards> Cards { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
       
    }
}
