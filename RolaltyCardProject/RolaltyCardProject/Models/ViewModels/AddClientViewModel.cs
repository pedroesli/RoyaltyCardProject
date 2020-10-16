using Microsoft.AspNetCore.Mvc;
using RolaltyCardProject.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models.ViewModels
{
    public class AddClientViewModel
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }
        public Message Message { get; set; } = null;
    }
}
