using Microsoft.AspNetCore.Identity;

namespace PlantBuddy.Server.Identity;

public class PlantBuddyUser : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
