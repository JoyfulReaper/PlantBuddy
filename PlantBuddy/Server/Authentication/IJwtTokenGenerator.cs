using ErrorOr;
using PlantBuddy.Server.Identity;
using System.Security.Claims;

namespace PlantBuddy.Server.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateRefreshToken();
    string GenerateToken(PlantBuddyUser user);
    ErrorOr<ClaimsPrincipal> GetPrincipalFromExpiredToke(string token);
}