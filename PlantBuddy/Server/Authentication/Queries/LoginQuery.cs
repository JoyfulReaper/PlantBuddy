using ErrorOr;
using MediatR;
using PlantBuddy.Server.Authentication.Common;

namespace PlantBuddy.Server.Authentication.Queries;

public record LoginQuery (
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;