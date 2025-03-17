using FluentValidation;
using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Result.UserError;

namespace OLMS.Application.Feature.User;

public sealed class CreateUserValidation : AbstractValidator<RegisterUserCommand>
{
    public CreateUserValidation()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Username cannot be empty");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
        RuleFor(u => u.FullName).NotEmpty().WithMessage("Full name cannot be empty");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
    }
}

public sealed record RegisterUserCommand(string Username,
                                string Password,
                                string FullName,
                                string Email,
                                int Age,
                                Role Role) : IRequest<Result<Guid>>
{
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork; 
        _userRepository = userRepository;
    }
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var username = Username.Create(request.Username);

        if (!await _userRepository.IsUsernameUniqueAsync(username, cancellationToken))
            return NonUniqueUsername;

        var email = Email.Create(request.Email);

        if (!await _userRepository.IsEmailUniqueAsync(email, cancellationToken))
            return NonUniqueEmail;

        var password = Password.Create(request.Password);
        var fullName = FullName.Create(request.FullName);

        var user = UserBase.CreateUser(Guid.NewGuid(), 
                                       username, 
                                       password, 
                                       fullName,
                                       email, 
                                       request.Age,
                                       request.Role);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}

