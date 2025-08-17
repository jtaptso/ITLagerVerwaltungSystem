using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ITLagerVerwaltungSystem.Core.Domain;

namespace ITLagerVerwaltungSystem.Infrastructure
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Removed custom Users DbSet to avoid conflict with IdentityUser
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MovementLog> MovementLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order-Material relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Materials)
                .WithOne(m => m.Order);

            // Material-MovementLog relationship
            modelBuilder.Entity<Material>()
                .HasMany(m => m.MovementLogs)
                .WithOne(ml => ml.Material);

            // Removed Notification-User relationship for Identity compatibility

            // Value objects as owned types
            modelBuilder.Entity<Material>().OwnsOne(m => m.SerialNumber);
            modelBuilder.Entity<Material>().OwnsOne(m => m.MaterialType);
            modelBuilder.Entity<Material>().OwnsOne(m => m.Condition);
            // Quantity is mapped automatically as an int property
        }

        // Seed initial data for testing (user seeding removed; use Identity API for users/roles)
    }
}
