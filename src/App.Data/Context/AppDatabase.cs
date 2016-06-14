﻿using App.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Context
{
    public class AppDatabase
    {
        const string DEFAULT_ADMIN_USER = "Admin";
        const string DEFAULT_ADMIN_PASSWORD = "123123";
        const string DEFAULT_ADMINISTRATOR_ROLE = "Administrator";

        public static async void InitializeDatabase(IServiceProvider serviceProvider, bool isProduction)
        {

            // Create App database tables
            using (var context = serviceProvider.GetRequiredService<AppDbContext>())
            {
                if (isProduction)
                {
                    // FOR PROD - Use Migrations
                    await context.Database.MigrateAsync();
                }
                else
                {
                    //// FOR NON PROD - Use Auto Database Generation without the need for Migrations
                    await context.Database.EnsureCreatedAsync();

                    //// Seed Development Data
                    //var todoRepo = serviceProvider.GetRequiredService<ITodoItemRepository>();
                    //var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

                    //if (await todoRepo.ReadAll().AnyAsync())
                    //{
                    //    return; // Database has already been seeded
                    //};

                    //todoRepo.CreateBulk(SeedTodoItemsData());
                    //await unitOfWork.SaveChangesAsync();
                }
            }

        }
        public static async void EnsureIdentityDatabaseExists(IServiceProvider serviceProvider, bool isProduction)
        {
            using (var identityContext = new ApplicationContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
            {
                // Ensure the identity tables are created and up-to-date
                if (isProduction)
                {
                    await identityContext.Database.MigrateAsync();
                }
                else
                {
                    await identityContext.Database.EnsureCreatedAsync();
                }

                // Ensure the admin user exists
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (await userManager.FindByNameAsync(DEFAULT_ADMIN_USER) == null)
                {
                    // If not create it
                    var adminUser = new ApplicationUser { UserName = DEFAULT_ADMIN_USER };
                    var result = await userManager.CreateAsync(adminUser, DEFAULT_ADMIN_PASSWORD);

                    // TODO: Gonz introduce correct logging!
                    //if (!result.Succeeded)
                    //{
                    //	Console.WriteLine("Could not create default 'Admin' user");
                    //	foreach (var error in result.Errors)
                    //	{
                    //		Console.WriteLine($"{error.Code}: {error.Description}");
                    //	}
                    //}
                };

                // Ensure the administrators role exists
                var roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (await roleManger.FindByNameAsync(DEFAULT_ADMINISTRATOR_ROLE) == null)
                {
                    // If not create it
                    var administratorsRole = new IdentityRole { Name = DEFAULT_ADMINISTRATOR_ROLE };
                    var result = await roleManger.CreateAsync(administratorsRole);

                    // Add the admin user to the new role
                    var adminUser = await userManager.FindByNameAsync(DEFAULT_ADMIN_USER);
                    await userManager.AddToRoleAsync(adminUser, DEFAULT_ADMINISTRATOR_ROLE);

                    // TODO: Gonz introduce correct logging!
                }
            }
        }
    }
}
