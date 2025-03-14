using FluentValidation;
using OLMS.Application.Feature.User;

namespace OLMS.Application.Features.User;

public sealed class LoginUserValidation : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidation()
    {
        RuleFor(u => u.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty");
        RuleFor(u => u.Password).NotEmpty().NotNull().WithMessage("Password cannot be empty");
    }
}
