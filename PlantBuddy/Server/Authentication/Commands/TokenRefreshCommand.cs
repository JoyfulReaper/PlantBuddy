using ErrorOr;
using MediatR;
using PlantBuddy.Server.Authentication.Common;

namespace PlantBuddy.Server.Authentication.Commands;

public record TokenRefreshCommand(
    string token,
    string refreshToken) : IRequest<ErrorOr<AuthenticationResult>>;