using CodeForge.Common.ViewModels.RequestModels;
using FluentValidation;

namespace CodeForge.Api.Application.Features.Commands.User.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(u => u.Email).NotNull().EmailAddress().WithMessage("{PropertyName} not a valid email address");

        RuleFor(u => u.Password).NotNull().MinimumLength(6).WithMessage("{PropertyName} must at least be {MinLenght} characters");
    }
}
