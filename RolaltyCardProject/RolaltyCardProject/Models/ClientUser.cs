using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models
{
    public class ClientUser : IdentityUser
    {
        [Required]
        public string Cpf { get; set; }
        public ICollection<ClientCards> Cards { get; set; }
    }
}
