using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string AplicationUserId { get; set; }

        [ForeignKey("AplicationUserId")]
        public ApplicationUser AplicationUser { get; set; }
        [ForeignKey("LoyaltyCardId")]
        public LoyaltyCard LoyaltyCard { get; set; }

        [DefaultValue(0)]
        public int Points { get; set; }
        public string VoucherCode { get; set; }
        [DefaultValue(false)]
        public bool IsRedeemed { get; set; }
    }
}
