using RolaltyCardProject.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RolaltyCardProject.Models.ViewModels
{
    public class AddPointViewModel
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }
        public ClientCards ClientCard { get; set; }
        public string ClientCardName { get; set; }
        public Message Message { get; set; } = null;
    }
}
