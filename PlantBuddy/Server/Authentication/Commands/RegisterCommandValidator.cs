using FluentValidation;

namespace PlantBuddy.Server.Authentication.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(2);

        RuleFor(r => r.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(2);

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
