using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pizza_hub.Data;
using pizza_hub.Data.Models.Identity;

namespace pizza_hub.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 2;
            options.Password.RequiredUniqueChars = 0;

        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
