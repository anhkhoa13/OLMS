using MediatR;
using OLMS.Application.Services;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Result.UserError;

namespace OLMS.Application.Feature.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var username = Username.Create(request.Username);
        var user = await _userRepository.GetByUsernameAsync(username, cancellationToken);

        if (user is null)
            return CannotLogin;

        if (!user.Password.Equals(request.Password))
            return CannotLogin;

        var token = _jwtService.GenerateToken(user);

        return token;
    }
}

