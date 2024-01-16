using GlobalRenov.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalRenov.Datas
{
    public class DefaultContext : DbContext
    {
        
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIntervention>()
                .HasKey(ui => new { ui.UserId, ui.InterventionId });

            modelBuilder.Entity<UserIntervention>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInterventions)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<UserIntervention>()
                .HasOne(ui => ui.Intervention)
                .WithMany(i => i.UserInterventions)
                .HasForeignKey(ui => ui.InterventionId);


            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.User)
              .WithMany(u => u.UserRoles)
              .HasForeignKey(ur => ur.UserId);
        }
        
    }
}
