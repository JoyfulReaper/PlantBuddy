using FluentValidation;

namespace PlantBuddy.Server.Authentication.Queries;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(l => l.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
