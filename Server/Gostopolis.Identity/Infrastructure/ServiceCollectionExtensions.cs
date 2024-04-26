namespace Gostopolis.Identity.Infrastructure;

using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// Extensions for the current application services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Declares identity options
    /// </summary>
    /// <param name="services">Current application services</param>
    /// <returns></returns>
    public static IServiceCollection AddUserStorage(
        this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
