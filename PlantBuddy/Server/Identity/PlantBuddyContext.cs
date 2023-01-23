using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlantBuddy.Server.Identity;

public class PlantBuddyContext : IdentityDbContext<PlantBuddyUser>
{
    public PlantBuddyContext(DbContextOptions<PlantBuddyContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(PlantBuddyContext).Assembly);
        
        base.OnModelCreating(builder);
    }
}

