namespace Gostopolis.Identity.Data;

using Gostopolis.Services;
using Microsoft.AspNetCore.Identity;
using Models;
using static Constants;

/// <inheritdoc />
public class IdentityDataSeeder(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) 
    : IDataSeeder
{
    /// <inheritdoc />
    public void SeedData()
    {
        if (roleManager.Roles.Any())
        {
            return;
        }

        Task
            .Run(async () =>
            {
                var adminRole = new IdentityRole(AdministratorRoleName);
                await roleManager.CreateAsync(adminRole);

                var adminUser = new User
                {
                    FirstName = "Гостополис",
                    LastName = "Администратор",
                    PhoneNumber = "+359895268528",
                    UserName = "admin@gostopolis.com",
                    Email = "admin@gostopolis.com",
                    EmailConfirmed = true,
                    SecurityStamp = "RandomSecurityStamp"
                };

                await userManager.CreateAsync(adminUser, "Admin_Gostopolis_2023");

                await userManager.AddToRoleAsync(adminUser, AdministratorRoleName);

                var partnerRole = new IdentityRole(PartnerRoleName);
                await roleManager.CreateAsync(partnerRole);
            })
            .GetAwaiter()
            .GetResult();

        if (userManager.Users.Count() > 1)
        {
            return;
        }

        Task
            .Run(async () =>
            {
                var firstUser = new User
                {
                    Id = "test-user-id-1",
                    FirstName = "Иван",
                    LastName = "Петров",
                    PhoneNumber = "+359895268528",
                    UserName = "ivan.petrov@test.com",
                    Email = "ivan.petrov@test.com",
                    EmailConfirmed = true,
                    SecurityStamp = "RandomSecurityStamp"
                };

                await userManager.CreateAsync(firstUser, "Ivan_123");

                var secondUser = new User
                {
                    Id = "test-user-id-2",
                    FirstName = "Стоян",
                    LastName = "Иванов",
                    PhoneNumber = "+359872218528",
                    UserName = "stoyan.ivanov@test.com",
                    Email = "stoyan.ivanov@test.com",
                    EmailConfirmed = true,
                    SecurityStamp = "RandomSecurityStamp"
                };

                await userManager.CreateAsync(secondUser, "Stoyan_123");

            })
            .GetAwaiter()
            .GetResult();
    }
}