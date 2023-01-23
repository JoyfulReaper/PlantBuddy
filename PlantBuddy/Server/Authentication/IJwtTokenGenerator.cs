using PlantBuddy.Server.Identity;

namespace PlantBuddy.Server.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(PlantBuddyUser user);
}