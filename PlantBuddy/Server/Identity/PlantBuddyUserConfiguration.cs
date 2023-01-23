using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlantBuddy.Server.Identity;

public class PlantBuddyUserConfiguration : IEntityTypeConfiguration<PlantBuddyUser>
{
    public void Configure(EntityTypeBuilder<PlantBuddyUser> builder)
    {
        builder.Property(p => p.FirstName)
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .HasMaxLength(100);
    }
}
