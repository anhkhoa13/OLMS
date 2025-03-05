using MediatR;
using OLMS.Domain.Entities;

namespace OLMS.Application.Feature.User;

public sealed record CreateUserCommand(string Username,
                                string Password,
                                string FullName, 
                                string Email, 
                                int Age,
                                Role Role) : IRequest<Guid>
{
}

