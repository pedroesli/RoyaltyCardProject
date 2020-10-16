using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models
{
    public class LoyaltyCard
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Card Name")]
        public string CardName { get; set; }
        [Display(Name = "Total Points")]
        public int TotalPoints { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        [Display(Name = "Prize Description")]
        public string PrizeDescription { get; set; }

        public string AplicationUserId { get; set; }
        [ForeignKey("AplicationUserId")]
        public AplicationUser AplicationUser { get; set; }
    }
}
