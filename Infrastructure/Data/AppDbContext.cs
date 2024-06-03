
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            //modelBuilder.Entity<IdentityRole>().HasNoKey();
            //////modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserRole>().HasNoKey();

            //modelBuilder.Entity<ApplicationUser>().HasNoKey();
            ////modelBuilder.Entity<ApplicationUser>()
            ////.HasMany(u => u.Logins)
            ////.WithOne()
            ////.HasForeignKey(l => l.UserId)
            ////.IsRequired();
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
