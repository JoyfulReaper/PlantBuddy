using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PlantBuddy.Server.Authentication.Common;
using PlantBuddy.Server.Identity;

namespace PlantBuddy.Server.Authentication.Commands;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly UserManager<PlantBuddyUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        UserManager<PlantBuddyUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new PlantBuddyUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors =
            result.Errors.Select(e => Error.Validation(e.Code, e.Description))
            .ToList();

            return errors;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
