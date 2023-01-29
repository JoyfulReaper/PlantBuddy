using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PlantBuddy.Server.Common.Errors;
using PlantBuddy.Server.Identity;

namespace PlantBuddy.Server.Authentication.Commands;

public class TokenRevokeHandler : IRequestHandler<TokenRevokeCommand, ErrorOr<Success>>
{
    private readonly UserManager<PlantBuddyUser> _userManager;

    public TokenRevokeHandler(UserManager<PlantBuddyUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<Success>> Handle(TokenRevokeCommand request, CancellationToken cancellationToken)
    {
        if (request.email is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var user = await _userManager.FindByEmailAsync(request.email);

        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);

        return new Success();
    }
}
