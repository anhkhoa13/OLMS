using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.UserError;

namespace OLMS.Application.Features.User;

public sealed record GetUserInfoCommand(string id) : IRequest<Result<UserBase>>
{
}

public class GetUserInfoCommandHandler : IRequestHandler<GetUserInfoCommand, Result<UserBase>>
{
    private readonly IUserRepository _userRepository;
    public GetUserInfoCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserBase>> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(Guid.Parse(request.id), cancellationToken);
        if (user is null)
            return UserNotFound;

        return user;
    }
}
