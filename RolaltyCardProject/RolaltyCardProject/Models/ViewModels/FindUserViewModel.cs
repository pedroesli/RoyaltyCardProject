using RolaltyCardProject.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models.ViewModels
{
    public class FindUserViewModel
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }
        public ICollection<ClientCards> ClientCards { get; set; }
        public Message Message { get; set; }
    }
}
