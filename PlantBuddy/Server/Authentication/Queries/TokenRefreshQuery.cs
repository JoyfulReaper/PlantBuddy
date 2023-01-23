using ErrorOr;
using MediatR;
using PlantBuddy.Server.Authentication.Common;

namespace PlantBuddy.Server.Authentication.Queries;

public record TokenRefreshQuery(
    string token,
    string refreshToken) : IRequest<ErrorOr<AuthenticationResult>>;