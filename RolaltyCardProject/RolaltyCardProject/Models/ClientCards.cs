using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models
{
    public class ClientCards
    {
        [Key]
        public int LoyaltyCardId { get; set; }
        [Key]
        public string ClientUserId { get; set; }  

        [ForeignKey("ClientUserId")]
        public ClientUser ClientUser { get; set; }
        [ForeignKey("LoyaltyCardId")]
        public LoyaltyCard LoyaltyCard { get; set; }
    }
}
