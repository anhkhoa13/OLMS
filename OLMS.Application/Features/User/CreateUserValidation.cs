using FluentValidation;
using OLMS.Application.Feature.User;
using OLMS.Domain.ValueObjects;

namespace OLMS.Application.Features.User;

internal sealed class CreateUserValidation : AbstractValidator<RegisterUserCommand>
{
    public CreateUserValidation()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Username cannot be empty");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
        RuleFor(u => u.FullName).NotEmpty().WithMessage("Full name cannot be empty");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
    }
}
