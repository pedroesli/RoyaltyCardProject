using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RolaltyCardProject.Models;

namespace RolaltyCardProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
        public DbSet<AplicationUser> AplicationUsers { get; set; }
        public DbSet<ClientCards> ClientCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientCards>()
                .HasKey(t => new { t.AplicationUserId, t.LoyaltyCardId });
        }
    }
}
