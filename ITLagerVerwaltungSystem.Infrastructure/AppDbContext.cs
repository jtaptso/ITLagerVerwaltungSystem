using Microsoft.EntityFrameworkCore;
using ITLagerVerwaltungSystem.Core.Domain;

namespace ITLagerVerwaltungSystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MovementLog> MovementLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Order relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            // Order-Material relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Materials)
                .WithOne(m => m.Order);

            // Material-MovementLog relationship
            modelBuilder.Entity<Material>()
                .HasMany(m => m.MovementLogs)
                .WithOne(ml => ml.Material);

            // Notification-User relationship
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // Value objects as owned types
            modelBuilder.Entity<Material>().OwnsOne(m => m.SerialNumber);
            modelBuilder.Entity<Material>().OwnsOne(m => m.MaterialType);
            modelBuilder.Entity<Material>().OwnsOne(m => m.Condition);
            // Quantity is mapped automatically as an int property
        }

        // Seed initial data for testing
        public static void SeedData(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var user1 = new User { Name = "manager1", Role = Role.Manager, Email = "manager1@example.com" };
                var user2 = new User { Name = "employee1", Role = Role.Employee, Email = "employee1@example.com" };
                var user3 = new User { Name = "staff1", Role = Role.Gast, Email = "staff1@example.com" };
                context.Users.AddRange(user1, user2, user3);
            }


            int materialId1 = 0, materialId2 = 0;
            int userId1 = 0;

            if (!context.Materials.Any())
            {
                var material1 = new Material { Model = "Laptop X", Status = MaterialStatus.New, Quantity = 10 };
                var material2 = new Material { Model = "Monitor Y", Status = MaterialStatus.Used, Quantity = 5 };
                context.Materials.AddRange(material1, material2);
                context.SaveChanges();
                materialId1 = material1.Id;
                materialId2 = material2.Id;
            }
            else
            {
                var first = context.Materials.First();
                var second = context.Materials.Skip(1).FirstOrDefault();
                materialId1 = first?.Id ?? 1;
                materialId2 = second?.Id ?? 2;
            }

            if (!context.Users.Any())
            {
                var user1 = new User { Name = "manager1", Role = Role.Manager, Email = "manager1@example.com" };
                var user2 = new User { Name = "employee1", Role = Role.Employee, Email = "employee1@example.com" };
                var user3 = new User { Name = "staff1", Role = Role.Gast, Email = "staff1@example.com" };
                context.Users.AddRange(user1, user2, user3);
                context.SaveChanges();
                userId1 = user1.Id;
            }
            else
            {
                userId1 = context.Users.First().Id;
            }

            if (!context.Orders.Any())
            {
                var order1 = new Order { UserId = userId1, Status = "Requested" };
                context.Orders.Add(order1);
            }

            if (!context.MovementLogs.Any())
            {
                var log1 = new MovementLog { MaterialId = materialId1, UserId = userId1, MovementType = MovementType.Procurement, Date = DateTime.Now };
                context.MovementLogs.Add(log1);
            }

            if (!context.Notifications.Any())
            {
                var notif1 = new Notification { UserId = userId1, Message = "Order approved", Date = DateTime.Now, IsRead = false };
                context.Notifications.Add(notif1);
            }

            context.SaveChanges();
        }
    }
}
