using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ITLagerVerwaltungSystem.Infrastructure
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            string[] roles = new[] { "Manager", "Employee", "WarehouseStaff", "Gast" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed a demo Employee user
            var employeeEmail = "Employee@itlager.com";
            var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
            if (employeeUser == null)
            {
                employeeUser = new IdentityUser
                {
                    UserName = employeeEmail,
                    Email = employeeEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(employeeUser, "Employee123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser, "Employee");
                }
            }

            // Seed Materials
            if (!dbContext.Materials.Any())
            {
                var material = new Core.Domain.Material
                {
                    Model = "Dell Latitude 5420",
                    Quantity = 10,
                    Status = Core.Domain.MaterialStatus.New
                };
                dbContext.Materials.Add(material);
                await dbContext.SaveChangesAsync();
            }

            // Seed Orders
            if (!dbContext.Orders.Any())
            {
                var order = new Core.Domain.Order
                {
                    UserId = employeeUser?.Id ?? "",
                    DateRequested = DateTime.UtcNow,
                    Status = "Pending"
                };
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
            }

            // Seed MovementLogs
            if (!dbContext.MovementLogs.Any())
            {
                var material = dbContext.Materials.FirstOrDefault();
                if (material != null)
                {
                    var movementLog = new Core.Domain.MovementLog
                    {
                        MaterialId = material.Id,
                        MovementType = Core.Domain.MovementType.Procurement,
                        Date = DateTime.UtcNow,
                        UserId = employeeUser?.Id ?? ""
                    };
                    dbContext.MovementLogs.Add(movementLog);
                    await dbContext.SaveChangesAsync();
                }
            }

            // Seed Notifications
            if (!dbContext.Notifications.Any())
            {
                var notification = new Core.Domain.Notification
                {
                    UserId = employeeUser?.Id ?? "",
                    Message = "Welcome to IT Lager!",
                    Date = DateTime.UtcNow,
                    IsRead = false
                };
                dbContext.Notifications.Add(notification);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
