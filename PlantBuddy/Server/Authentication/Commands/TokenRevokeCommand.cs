using ErrorOr;
using MediatR;

namespace PlantBuddy.Server.Authentication.Commands;

public record TokenRevokeCommand(string email) : IRequest<ErrorOr<Success>>;