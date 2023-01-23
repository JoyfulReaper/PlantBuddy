using ErrorOr;
using MediatR;

namespace PlantBuddy.Server.Authentication.Queries;

public record TokenRevokeQuery(string email) : IRequest<ErrorOr<Success>>;