using Mapster;
using PlantBuddy.Server.Authentication.Commands;
using PlantBuddy.Server.Authentication.Common;
using PlantBuddy.Server.Authentication.Queries;
using PlantBuddy.Shared.Contracts.Authentication;

namespace PlantBuddy.Server.Common.Mapping;

public class AuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<TokenRefreshRequest, TokenRefreshCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id)
            .Map(dest => dest, src => src.User);
    }
}
