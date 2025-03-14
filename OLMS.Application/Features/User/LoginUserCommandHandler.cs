using MediatR;
using OLMS.Application.Services;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Result.UserError;

namespace OLMS.Application.Feature.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserBase>>
{
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserBase>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var username = Username.Create(request.Username);
        var user = await _userRepository.GetByUsernameAsync(username, cancellationToken);

        if (user is null)
            return CannotLogin;

        var password = Password.Create(request.Password);
        if (!user.Password.Equals(password))
            return CannotLogin;

        return user;
    }
}

