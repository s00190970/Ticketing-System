using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Database.Context
{
    public class DatabaseInitializer
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DatabaseContext context)
        {
            await SeedRoles(roleManager);
            await SeedUsers(context, userManager);
            await SeedTicketProperties(context);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        private static async Task SeedUsers(DatabaseContext context,UserManager<User> userManager)
        {
            var user = new User
            {
                Email = "admin@admin.com",
                UserName = "admin",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                PasswordHasher<User> ph = new PasswordHasher<User>();
                user.PasswordHash = ph.HashPassword(user, "password123");

                var userStore = new UserStore<User>(context);
                var result = userStore.CreateAsync(user);

                if (result.Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedTicketProperties(DatabaseContext context)
        {
            if (!context.Priorities.Any())
            {
                await context.Priorities.AddRangeAsync(new List<Priority>()
                {
                    new Priority("Low"),
                    new Priority("Medium"),
                    new Priority("High")
                });
            }

            if (!context.ServiceTypes.Any())
            {
                await context.ServiceTypes.AddRangeAsync(new List<ServiceType>()
                {
                    new ServiceType("Repair"),
                    new ServiceType("Debug")
                });

            }

            if (!context.Statuses.Any())
            {
                await context.Statuses.AddRangeAsync(new List<Status>()
                {
                    new Status("Waiting"),
                    new Status("In Progress"),
                    new Status("Testing"),
                    new Status("Done")
                });
            }

            if (!context.TicketTypes.Any())
            {
                await context.TicketTypes.AddRangeAsync(new List<TicketType>()
                {
                    new TicketType("Tech"),
                    new TicketType("Business")
                });
            }

            if (!context.Settings.Any())
            {
                await context.Settings.AddRangeAsync(new List<Setting>()
                {
                    new Setting {Name = "CanModifyTicketType", Enabled = true},
                    new Setting {Name = "CanModifyCustomerName", Enabled = true},
                    new Setting {Name = "CanModifyServiceType", Enabled = true}
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
