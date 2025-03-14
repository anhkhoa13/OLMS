using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Result;

namespace OLMS.Application.Feature.User;

public sealed record LoginUserCommand(string Username,
                                      string Password) : IRequest<Result<UserBase>>
{
}
