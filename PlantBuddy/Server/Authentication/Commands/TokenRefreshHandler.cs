using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PlantBuddy.Server.Authentication.Common;
using PlantBuddy.Server.Common.Errors;
using PlantBuddy.Server.Common.Services;
using PlantBuddy.Server.Identity;
using System.Security.Claims;

namespace PlantBuddy.Server.Authentication.Commands;

public class TokenRefreshHandler : IRequestHandler<TokenRefreshCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<PlantBuddyUser> _userManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public TokenRefreshHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<PlantBuddyUser> userManager,
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(TokenRefreshCommand request, CancellationToken cancellationToken)
    {
        var principalResult = _jwtTokenGenerator.GetPrincipalFromExpiredToke(request.token);
        if (principalResult.IsError)
        {
            return principalResult.Errors;
        }

        var username = principalResult.Value.Identity.Name;
        var user = await _userManager.FindByNameAsync(username);

        if (user is null ||
            user.RefreshToken != request.refreshToken ||
            user.RefreshTokenExpiryTime <= _dateTimeProvider.UtcNow)
        {
            return Errors.Authentication.InvalidToken;
        }

        var newToken = _jwtTokenGenerator.GenerateToken(user);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = _dateTimeProvider.UtcNow.AddDays(_jwtSettings.RefreshExpirationDays);

        await _userManager.UpdateAsync(user);

        return new AuthenticationResult(user, newToken, newRefreshToken);
    }
}
