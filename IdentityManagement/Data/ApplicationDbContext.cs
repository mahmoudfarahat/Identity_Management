using IdentityManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasDefaultSchema("");

            builder.Entity<ApplicationUser>().ToTable("Users","Secuitry").Ignore(e => e.PhoneNumberConfirmed);
            builder.Entity<IdentityRole>().ToTable("Roles", "Secuitry");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Secuitry");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Secuitry");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Secuitry");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Secuitry");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Secuitry");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Secuitry");






        }


    }
}
