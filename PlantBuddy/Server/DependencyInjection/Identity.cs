using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlantBuddy.Server.Identity;

namespace PlantBuddy.Server.DependencyInjection;

public static class Identity
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        // Identity
        services.AddDbContext<PlantBuddyContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("PlantBuddy"));
        });

        services.AddIdentity<PlantBuddyUser, IdentityRole>(opts =>
        {
            opts.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<PlantBuddyContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
