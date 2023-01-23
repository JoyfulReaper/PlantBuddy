using ErrorOr;
using MediatR;
using PlantBuddy.Server.Authentication.Common;

namespace PlantBuddy.Server.Authentication.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;