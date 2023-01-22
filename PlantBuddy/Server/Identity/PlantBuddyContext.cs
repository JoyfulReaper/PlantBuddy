using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlantBuddy.Server.Identity;

public class PlantBuddyContext : IdentityDbContext<PlantBuddyUser>
{
    public PlantBuddyContext(DbContextOptions<PlantBuddyContext> options)
        : base(options) { }
}

