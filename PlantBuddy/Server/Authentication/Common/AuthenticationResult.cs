using PlantBuddy.Server.Identity;

namespace PlantBuddy.Server.Authentication.Common;

public record AuthenticationResult(
    PlantBuddyUser User,
    string Token,
    string RefreshToken);