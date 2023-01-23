using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PlantBuddy.Server.Authentication.Common;
using PlantBuddy.Server.Common.Errors;
using PlantBuddy.Server.Common.Services;
using PlantBuddy.Server.Identity;

namespace PlantBuddy.Server.Authentication.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<PlantBuddyUser> _userManager;
    private readonly SignInManager<PlantBuddyUser> _signInManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<PlantBuddyUser> userManager,
        SignInManager<PlantBuddyUser> signInManager,
        IOptions<JwtSettings> jwtSettings,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        PlantBuddyUser? user = await _userManager.FindByEmailAsync(request.Email);
        if(user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if(!result.Succeeded)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = _dateTimeProvider.UtcNow.AddDays(_jwtSettings.RefreshExpirationDays);
        await _userManager.UpdateAsync(user);

        return new AuthenticationResult(user, token, refreshToken);
    }
}
